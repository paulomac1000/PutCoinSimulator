using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    public class GeneratorWorker : BaseWorker, IGeneratorWorker
    {
        private readonly IPersonNameGenerator personGenerator;
        private readonly IHashGenerator hashGenerator;

        public GeneratorWorker()
        {
            personGenerator = new PersonNameGenerator();
            hashGenerator = new HashGenerator();
        }

        public override void Work()
        {
            
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
            var block = new Block
            {
                Data = new BlockData
                {
                    Amount = 50,
                    Receiver = GetRandomClient().OwnerName
                },
                PreviousBlockHash = Settings.FirstBlockPreviousBlockHashValue
            };
            block.Hash = hashGenerator.GenerateHashFromBlock(block.Data, block.PreviousBlockHash);

            Datas.GenesisBlock = null;
        }

        public void GenerateProperTransfer()
        {
            throw new NotImplementedException();
        }

        public void GenerateUnproperTransaction(FakeTransactionType type)
        {
            throw new NotImplementedException();
        }

        private static Pocket GetRandomClient()
        {
            var random = new SecureRandom();
            return Datas.Pockets.ElementAt(random.Next(0, Settings.NumbersOfClients));
        }
    }
}
