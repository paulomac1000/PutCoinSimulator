using System.Collections.Generic;
using Common.Interfaces;
using Models;

namespace VisualisingThread.Interfaces
{
    /// <summary>
    /// Third thread
    /// </summary>
    public interface IVisualisingWorker : IWork
    {
        IEnumerable<Block> GetBlockchain();
        IEnumerable<Transaction> GetCommisionedTransactions();
        IEnumerable<Transaction> GetUnreleasedTransactions();
        IEnumerable<PocketAmount> GetPockets();
    }
}
