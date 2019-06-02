using System;
using Common;
using GeneratorThread.Interfaces;
using Models.AddingThread.Enums;

namespace GeneratorThread
{
    /// <summary>
    /// First thread
    /// </summary>
    public class GeneratorWorker : BaseWorker, IGeneratorWorker
    {
        public override void Work()
        {
            throw new NotImplementedException();
        }

        public void GenerateClientsPocket()
        {
            throw new NotImplementedException();
        }

        public void CreateGenesisBlock()
        {
            throw new NotImplementedException();
        }

        public void GenerateProperTransfer()
        {
            throw new NotImplementedException();
        }

        public void GenerateUnproperTransaction(FakeTransactionType type)
        {
            throw new NotImplementedException();
        }
    }
}
