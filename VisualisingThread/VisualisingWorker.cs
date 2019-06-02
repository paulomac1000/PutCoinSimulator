using System;
using System.Collections.Generic;
using Common;
using Models;
using VisualisingThread.Interfaces;

namespace VisualisingThread
{
    /// <summary>
    /// Third thread
    /// </summary>
    public class VisualisingWorker : BaseWorker, IVisualisingWorker
    {
        public override void Work()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Block> GetBlockchain()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Transaction> GetCommisionedTransactions()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Transaction> GetUnreleasedTransactions()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PocketAmount> GetPockets()
        {
            throw new NotImplementedException();
        }
    }
}
