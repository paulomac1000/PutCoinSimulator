using System.Collections.Generic;
using Common.Interfaces;
using Models;

namespace AddingThread.Interfaces
{
    /// <summary>
    /// Second Thread
    /// </summary>
    public interface IAddingWorker : IWork
    {
        void VerifyTransactions(List<Transaction> transactions);
        string FindNonceForBlock(Block block);
        void AddBlockToBlockchain(Block block);
    }
}
