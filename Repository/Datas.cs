using System.Collections.Generic;
using Models;
using Models.AddingThread;

namespace Repository
{
    public static class Datas
    {
        public static IEnumerable<Pocket> Pockets { get; set; }
        public static Queue<Transaction> WaitingTransactions { get; set; }
        public static Queue<Transaction> RejectedTransactions { get; set; }
        public static Block GenesisBlock { get; set; }
        public static byte[] Salt { get; set; }
    }
}
