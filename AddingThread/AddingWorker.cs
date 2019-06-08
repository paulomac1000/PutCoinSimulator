using System;
using System.Collections.Generic;
using System.Threading;
using AddingThread.Interfaces;
using Common;
using Models;
using Repository;

namespace AddingThread
{
    /// <summary>
    /// Second Thread
    /// </summary>
    public class AddingWorker : IAddingWorker
    {
        public string Name => nameof(AddingWorker);

        public void Work()
        {
            Datas.RejectedTransactions = new List<Transaction>();
            Datas.CommisionedTransactions = new List<Transaction>();

            while (Settings.AppStarted)
            {
                Thread.Sleep(10000);
            }
        }

        public bool VerifyTransaction()
        {
            throw new NotImplementedException();
        }

        public string FindNonceForBlock()
        {
            throw new NotImplementedException();
        }

        public void AddBlockToBlockchain()
        {
            throw new NotImplementedException();
        }
    }
}
