using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Common;
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
        private readonly SecureRandom secureRandom;

        public string Name => nameof(GeneratorWorker);

        public GeneratorWorker()
        {
            personGenerator = new PersonNameGenerator();
            secureRandom = new SecureRandom();
        }

        public void Work()
        {
            Datas.Blockchain = new List<Block>();
            Datas.WaitingTransactions = new SynchronizedCollection<Transaction>();

            GenerateClientsPocket();
            CreateTransactionForGenesisBlock();

            while (Settings.AppStarted)
            {
                if (secureRandom.Next(1, 100) <= Settings.ChanceForGenerateFakeTransactionInPercent)
                    GenerateUnproperTransaction((FakeTransactionType)secureRandom.Next(1,4));
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
                    OwnerName = personGenerator.GenerateRandomFirstAndLastName()
                };
                pockets.Add(pocket);
            }

            Datas.Pockets = pockets;
        }

        public void CreateTransactionForGenesisBlock()
        {
            var transaction = new Transaction
            {
                Time = DateTime.Now,
                Sender = Settings.FirstBlockSenderName,
                Amount = 50,
                Receiver = GetRandomClient().OwnerName
            };
            AddTransaction(transaction);
            Thread.Sleep(1500);
        }

        public void GenerateProperTransfer()
        {
            var pocketsWithMoney = Helpers.GetPocketWhichHasMoney().ToList();
            if (!pocketsWithMoney.Any())
                return;

            var sender = pocketsWithMoney.ElementAt(secureRandom.Next(0, pocketsWithMoney.Count - 1));
            var receiver = GetRandomReceiver(sender);

            var balance = Helpers.GetAccountBalanceByOwnerName(sender.OwnerName);

            var transaction = new Transaction
            {
                Time = DateTime.Now,
                Sender = sender.OwnerName,
                Receiver = receiver.OwnerName,
                Amount = Math.Round(secureRandom.Next(1, (int)(balance * 10)) / 10.0, 1)
            };
            AddTransaction(transaction);
        }

        public void GenerateUnproperTransaction(FakeTransactionType type)
        {
            var sender = GetRandomClient();
            var receiver = GetRandomReceiver(sender);

            Transaction transaction = null;
            switch (type)
            {
                case FakeTransactionType.BadSignature:
                    transaction = new Transaction
                    {
                        Time = DateTime.Now,
                        Sender = sender.OwnerName,
                        Receiver = sender.OwnerName,
                        Amount = Math.Round(secureRandom.Next(1, 30) / 10.0, 1) //0.1 - 3.0
                    };
                    break;
                case FakeTransactionType.DoubleSpending:
                    transaction = new Transaction
                    {
                        Time = DateTime.Now,
                        Sender = sender.OwnerName,
                        Receiver = receiver.OwnerName,
                        Amount = Math.Round(secureRandom.Next(1, 30) / 10.0, 1) //0.1 - 3.0
                    };
                    AddTransaction(transaction);
                    break;
                case FakeTransactionType.BadAmount:
                    transaction = new Transaction
                    {
                        Time = DateTime.Now,
                        Sender = sender.OwnerName,
                        Receiver = receiver.OwnerName,
                        Amount = Math.Round(-1 * (secureRandom.Next(1, 30) / 10.0), 1) //0.1 - 3.0
                    };
                    break;
                default:
                    throw new NotImplementedException();
            }
            AddTransaction(transaction);
        }

        private static void AddTransaction(Transaction transaction)
        {
            Datas.WaitingTransactions.Add(transaction);
        }

        private Pocket GetRandomClient()
        {
            return Datas.Pockets.ElementAt(secureRandom.Next(0, Settings.NumbersOfClients));
        }

        private Pocket GetRandomReceiver(Pocket sender)
        {
            var receiver = GetRandomClient();
            while (sender == receiver)
                receiver = GetRandomClient();

            return receiver;
        }
    }
}
