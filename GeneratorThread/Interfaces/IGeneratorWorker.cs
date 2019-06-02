using Common.Interfaces;
using Models.AddingThread.Enums;

namespace GeneratorThread.Interfaces
{
    public interface IGeneratorWorker : IWork
    {
        void GenerateClientsPocket();
        void CreateGenesisBlock();
        void GenerateProperTransfer();
        void GenerateUnproperTransaction(FakeTransactionType type);
    }
}
