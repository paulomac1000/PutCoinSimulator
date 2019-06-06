using System;
using System.Collections.Generic;
using System.Linq;
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
    public class GeneratorWorker : BaseWorker, IGeneratorWorker
    {
        private readonly PersonNameGenerator personGenerator;

        public GeneratorWorker()
        {
            personGenerator = new PersonNameGenerator();
        }

        public override void Work()
        {
            
        }

        public void GenerateClientsPocket()
        {
            var pocekts = new List<Pocket>();

            for (var i = 0; i < Settings.NumbersOfClients; i++)
            {
                var pocket = new Pocket
                {
                    OwnerName = personGenerator.GenerateRandomFirstAndLastName(),
                    PrivateKey = Guid.NewGuid().ToString(),
                    PublicKey = Guid.NewGuid().ToString()
                };
                pocekts.Add(pocket);
            }

            Datas.Pockets = pocekts;
        }

        public void CreateGenesisBlock()
        {
            Datas.GenesisBlock = new Block
            {
                BlockHash = Guid.NewGuid().ToString(),
                Data = new BlockData
                {
                    Amount = 50,
                    Receiver = GetRandomClient().OwnerName
                },
                Hash = null, //generate hash?
                PreviousBlockHash = string.Empty
            };
        }

        public void GenerateProperTransfer()
        {
            throw new NotImplementedException();
        }

        public void GenerateUnproperTransaction(FakeTransactionType type)
        {
            throw new NotImplementedException();
        }

        private Pocket GetRandomClient()
        {
            var random = new Random(DateTime.Now.Millisecond);
            return Datas.Pockets.ElementAt(random.Next(0, Settings.NumbersOfClients));
        }
    }
}
