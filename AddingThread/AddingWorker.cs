using System;
using AddingThread.Interfaces;
using Common;

namespace AddingThread
{
    /// <summary>
    /// Second Thread
    /// </summary>
    public class AddingWorker : BaseWorker, IAddingWorker
    {
        public override void Work()
        {
            throw new NotImplementedException();
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
