using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PutCoinSimulator.Tests
{
    [TestClass]
    public class SecureRandomTests
    {
        private readonly SecureRandom secureRandom;
        public SecureRandomTests()
        {
            secureRandom = new SecureRandom();
        }

        [TestMethod]
        public void GenerateRandom_Success()
        {
            var list = new List<int>();

            for (var i = 0; i < 100; i++)
            {
                list.Add(secureRandom.Next());
            }

            var groupedList = list.GroupBy(i => i);
            Assert.IsTrue(groupedList.Count() > 1);
        }

        [TestMethod]
        public void GenerateRandomFromRangte_Success()
        {
            var list = new List<int>();

            for (var i = 0; i < 100; i++)
            {
                list.Add(secureRandom.Next(1, 100));
            }

            var groupedList = list.GroupBy(i => i).ToList();
            Assert.IsTrue(groupedList.Count() > 1);
            Assert.IsTrue(groupedList.All(i => i.Key >= 1 && i.Key <= 100));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GenerateRandom_MAxLessThanMin_Failed()
        {
            secureRandom.Next(10,1);
        }
    }
}
