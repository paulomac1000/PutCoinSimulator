using System.Collections.Generic;
using Common.Interfaces;
using Models;
using Models.AddingThread;

namespace VisualisingThread.Interfaces
{
    /// <summary>
    /// Third thread
    /// </summary>
    public interface IVisualisingWorker : IWork
    {
        IEnumerable<Block> GetBlockchain { get; }
        IEnumerable<Transaction> GetCommisionedTransactions { get; }
        IEnumerable<Transaction> GetUnreleasedTransactions { get; }
        IEnumerable<Pocket> GetPockets { get; }
    }
}
