using System.Collections.Generic;
using Common;
using Common.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;

namespace PutCoinSimulator.Tests
{
    [TestClass]
    public class BlockTests
    {
        private readonly IHashGenerator hashGenerator;

        public BlockTests()
        {
            hashGenerator = new HashGenerator();        
        }

        [TestMethod]
        public void Salt_Success()
        {
            var salt = hashGenerator.CreateSalt();
            Assert.IsNotNull(salt);
        }

        [TestMethod]
        public void HashFromBlock_Success()
        {
            var blockData = new BlockData
            {
                Transactions = new List<Transaction>
                {
                    new Transaction
                    {
                        Sender = "bbb",
                        Receiver = "aaa",
                        Amount = 10.5
                    }
                }
            };

            var previousBlockHash = "testowy_hash";

            var hash = hashGenerator.GenerateHashFromBlock(blockData, previousBlockHash);
            Assert.IsNotNull(hash);
        }
    }
}
