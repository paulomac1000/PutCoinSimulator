using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Common;
using Common.Interfaces;
using GeneratorThread.Interfaces;
using Models;
using Models.AddingThread;
using Models.AddingThread.Enums;
using RandomNameGeneratorLibrary;
using Repository;

namespace GeneratorThread
{
    /// <summary>
    /// First thread
    /// </summary>
    public class GeneratorWorker : IGeneratorWorker
    {
        private readonly IPersonNameGenerator personGenerator;
        private readonly IHashGenerator hashGenerator;
        private readonly SecureRandom secureRandom;

        public string Name => nameof(GeneratorWorker);

        public GeneratorWorker()
        {
            personGenerator = new PersonNameGenerator();
            hashGenerator = new HashGenerator();
            secureRandom = new SecureRandom();
        }

        public void Work()
        {
            Datas.Blockchain = new List<Block>();
            Datas.WaitingTransactions = new Queue<Transaction>();

            GenerateClientsPocket();
            
            CreateGenesisBlock();
            while (Settings.AppStarted)
            {
                if (secureRandom.Next(1, 100) <= Settings.ChanceForGenerateFakeTransactionInPercent)
                    GenerateUnproperTransaction((FakeTransactionType)secureRandom.Next(1,3));
                else
                    GenerateProperTransfer();

                Thread.Sleep(secureRandom.Next(Settings.GenerateTransactionMinDelayInSeconds, Settings.GenerateTransactionMaxDelayInSeconds) * 1000);
            }
        }

        public void GenerateClientsPocket()
        {
            var pockets = new List<Pocket>();

            for (var i = 0; i < Settings.NumbersOfClients; i++)
            {
                var pocket = new Pocket
                {
                    OwnerName = personGenerator.GenerateRandomFirstAndLastName(),
                    PrivateKey = Guid.NewGuid().ToString(),
                    PublicKey = Guid.NewGuid().ToString()
                };
                pockets.Add(pocket);
            }

            Datas.Pockets = pockets;
        }

        public void CreateGenesisBlock()
        {
            var blockData = new BlockData
            {
                Transactions = new List<Transaction>
                {
                    new Transaction
                    {
                        Sender = Settings.FirstBlockSenderName,
                        Amount = 50,
                        Receiver = GetRandomClient().OwnerName
                    }
                }
            };

            var block = new Block
            {
                Data = blockData,
                PreviousBlockHash = Settings.FirstBlockPreviousBlockHashValue
            };
            block.Hash = hashGenerator.GenerateHashFromBlock(block.Data, block.PreviousBlockHash);

            Datas.Blockchain.Add(block);
        }

        public void GenerateProperTransfer()
        {
            var sender = GetRandomClient();
            var receiver = GetRandomReceiver(sender);

            var transaction = new Transaction
            {
                Sender = sender.OwnerName,
                Receiver = receiver.OwnerName,
                Amount = secureRandom.Next(1, 30) / 10.0 //0.1 - 3.0
            };
            Datas.WaitingTransactions.Enqueue(transaction);
        }

        public void GenerateUnproperTransaction(FakeTransactionType type)
        {
            var sender = GetRandomClient();
            var receiver = GetRandomReceiver(sender);

            Transaction transaction = null;
            if (type == FakeTransactionType.BadSignature)
            {
                transaction = new Transaction
                {
                    Sender = sender.OwnerName,
                    Receiver = sender.OwnerName,
                    Amount = secureRandom.Next(1, 30) / 10.0 //0.1 - 3.0
                };
            }
            else if(type == FakeTransactionType.DoubleSpending)
            {
                transaction = new Transaction
                {
                    Sender = sender.OwnerName,
                    Receiver = receiver.OwnerName,
                    Amount = secureRandom.Next(1, 30) / 10.0 //0.1 - 3.0
                };
                Datas.WaitingTransactions.Enqueue(transaction);
            }
            else if(type == FakeTransactionType.BadAmount)
            {
                transaction = new Transaction
                {
                    Sender = sender.OwnerName,
                    Receiver = receiver.OwnerName,
                    Amount = -secureRandom.Next(1, 30) / 10.0 //0.1 - 3.0
                };
            }
            else
            {
                throw new NotImplementedException();
            }
            Datas.WaitingTransactions.Enqueue(transaction);
        }

        private static Pocket GetRandomClient()
        {
            var random = new SecureRandom();
            return Datas.Pockets.ElementAt(random.Next(0, Settings.NumbersOfClients));
        }

        private static Pocket GetRandomReceiver(Pocket sender)
        {
            var receiver = GetRandomClient();
            while (sender == receiver)
                receiver = GetRandomClient();

            return receiver;
        }
    }
}
