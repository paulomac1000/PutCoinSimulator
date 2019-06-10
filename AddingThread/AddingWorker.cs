using System;
using System.Collections.Generic;
using System.Linq;
using AddingThread.Interfaces;
using Common;
using Common.Interfaces;
using Models;
using Models.AddingThread;
using Repository;

namespace AddingThread
{
    /// <summary>
    /// Second Thread
    /// </summary>
    public class AddingWorker : IAddingWorker
    {
        private readonly IHashGenerator hashGenerator;
        private readonly SecureRandom secureRandom;

        public AddingWorker()
        {
            hashGenerator = new HashGenerator();
            secureRandom = new SecureRandom();
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
                if (!transactions.Any())
                    continue;

                if (Datas.Blockchain != null && Datas.Blockchain.Any())
                    transactions.Add(GenerateRewardtransaction());

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

            var groupedTransactionsBySender = transactions.GroupBy(t => t.Sender);
            foreach (var group in groupedTransactionsBySender)
            {
                if (group.Key == Settings.FirstBlockSenderName || group.Key == Settings.RewardSenderName)
                    continue;

                var sumBySender = group.Sum(g => g.Amount);
                var accountbalace = Helpers.GetAccountBalanceByOwnerName(group.Key);
                if (!(accountbalace < sumBySender)) continue;

                foreach (var transaction in group)
                {
                    if (transaction.Amount > accountbalace)
                    {
                        Datas.RejectedTransactions.Add(transaction);
                        transactions.Remove(transaction);
                    }
                    else
                    {
                        accountbalace -= transaction.Amount;
                    }
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

        private Transaction GenerateRewardtransaction()
        {
            var rewardTransaction = new Transaction
            {
                Time = DateTime.Now,
                Sender = Settings.RewardSenderName,
                Receiver = GetRandomClient().OwnerName,
                Amount = Settings.AmountOfRewardTransaction
            };

            return rewardTransaction;
        }

        private Pocket GetRandomClient()
        {
            return Datas.Pockets.ElementAt(secureRandom.Next(0, Settings.NumbersOfClients + 1));
        }
    }
}
