using System;
using System.Collections.Generic;
using System.Linq;
using AddingThread.Interfaces;
using Common;
using Common.Interfaces;
using Models;
using Repository;

namespace AddingThread
{
    /// <summary>
    /// Second Thread
    /// </summary>
    public class AddingWorker : IAddingWorker
    {
        private readonly IHashGenerator hashGenerator;

        public AddingWorker()
        {
            hashGenerator = new HashGenerator();
        }

        public string Name => nameof(AddingWorker);

        public void Work()
        {
            Datas.RejectedTransactions = new List<Transaction>();
            Datas.CommisionedTransactions = new List<Transaction>();

            while (Settings.AppStarted)
            {
                if (Datas.WaitingTransactions == null || !Datas.WaitingTransactions.Any())
                    continue;

                var transactions = new List<Transaction>(Datas.WaitingTransactions.ToList());
                Datas.WaitingTransactions.Clear();

                VerifyTransactions(transactions);
                if(!transactions.Any())
                    continue;

                var blockData = new BlockData { Transactions = transactions };
                var block = new Block
                {
                    Data = blockData,
                    PreviousBlockHash = GetPreviousBlockHash()
                };

                block.Hash = FindNonceForBlock(block);
                AddBlockToBlockchain(block);
                Datas.CommisionedTransactions.AddRange(transactions);
            }
        }

        public void VerifyTransactions(List<Transaction> transactions)
        {
            var duplicatedTransactions = transactions.GroupBy(t => t)
                .Where(g => g.Count() > 1)
                .Select(g => g.First())
                .ToList();

            if (duplicatedTransactions.Any())
            {
                foreach (var transaction in duplicatedTransactions)
                {
                    Datas.RejectedTransactions.Add(transaction);
                    transactions.Remove(transaction);
                }
            }

            var badSignatureTransactions = transactions.Where(t => t.Sender == t.Receiver).ToList();
            if (badSignatureTransactions.Any())
            {
                foreach (var transaction in badSignatureTransactions)
                {
                    Datas.RejectedTransactions.Add(transaction);
                    transactions.Remove(transaction);
                }
            }

            var badAmount = transactions.Where(t => t.Amount <= 0).ToList();
            if (badAmount.Any())
            {
                foreach (var transaction in badAmount)
                {
                    Datas.RejectedTransactions.Add(transaction);
                    transactions.Remove(transaction);
                }
            }

            var whereOwnerHasNotEnoughMone = new List<Transaction>();
            foreach (var transaction in transactions)
            {
                if (transaction.Sender == Settings.FirstBlockSenderName)
                    continue;

                if (Helpers.GetAccountBalanceByOwnerName(transaction.Sender) < transaction.Amount)
                    whereOwnerHasNotEnoughMone.Add(transaction);
            }
            if (whereOwnerHasNotEnoughMone.Any())
            {
                foreach (var transaction in whereOwnerHasNotEnoughMone)
                {
                    Datas.RejectedTransactions.Add(transaction);
                    transactions.Remove(transaction);
                }
            }
        }

        public string FindNonceForBlock(Block block)
        {
            if (block == null)
                throw new NullReferenceException();

            var hash = hashGenerator.GenerateHashFromBlock(block.Data, block.PreviousBlockHash);
            return hash;
        }

        public void AddBlockToBlockchain(Block block)
        {
            Datas.Blockchain.Add(block);
        }

        private static string GetPreviousBlockHash()
        {
            var previousBlockHash = Datas.Blockchain.Any()
                ? Datas.Blockchain.Last().Hash
                : Settings.FirstBlockPreviousBlockHashValue;

            return previousBlockHash;
        }
    }
}
