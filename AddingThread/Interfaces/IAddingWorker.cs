using Common.Interfaces;

namespace AddingThread.Interfaces
{
    public interface IAddingWorker : IWork
    {
        bool VerifyTransaction();
        string FindNonceForBlock();
        void AddBlockToBlockchain();
    }
}
