using Common.Interfaces;

namespace AddingThread.Interfaces
{
    /// <summary>
    /// Second Thread
    /// </summary>
    public interface IAddingWorker : IWork
    {
        bool VerifyTransaction();
        string FindNonceForBlock();
        void AddBlockToBlockchain();
    }
}
