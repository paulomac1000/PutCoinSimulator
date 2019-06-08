using System.Collections.Generic;
using System.Threading;
using Common;
using Models;
using Models.AddingThread;
using Repository;
using VisualisingThread.Interfaces;

namespace VisualisingThread
{
    /// <summary>
    /// Third thread
    /// </summary>
    public class VisualisingWorker : IVisualisingWorker
    {
        public string Name => nameof(VisualisingWorker);

        public void Work()
        {
            while (Settings.AppStarted)
            {
                Thread.Sleep(10000);
            }
        }

        public IEnumerable<Block> GetBlockchain => Datas.Blockchain;

        // transakcje zatwierdzone
        public IEnumerable<Transaction> GetCommisionedTransactions => Datas.CommisionedTransactions;

        // transakcje niewydane
        public IEnumerable<Transaction> GetUnreleasedTransactions => Datas.WaitingTransactions;

        public IEnumerable<Pocket> GetPockets => Datas.Pockets;
    }
}
