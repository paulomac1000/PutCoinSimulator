﻿using Common.Interfaces;
using Models.AddingThread.Enums;

namespace GeneratorThread.Interfaces
{
    /// <summary>
    /// First thread
    /// </summary>
    public interface IGeneratorWorker : IWork
    {
        void GenerateClientsPocket();
        void CreateTransactionForGenesisBlock();
        void GenerateProperTransfer();
        void GenerateUnproperTransaction(FakeTransactionType type);
    }
}
