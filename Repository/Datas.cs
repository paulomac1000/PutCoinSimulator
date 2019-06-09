using System.Collections.Generic;
using Models;
using Models.AddingThread;

namespace Repository
{
    public static class Datas
    {
        public static IEnumerable<Pocket> Pockets { get; set; }
        public static List<Block> Blockchain { get; set; }
        public static bool WaitingTransactionsLocked { get; set; }
        public static SynchronizedCollection<Transaction> WaitingTransactions { get; set; }
        public static List<Transaction> CommisionedTransactions { get; set; }
        public static List<Transaction> RejectedTransactions { get; set; }
        public static byte[] Salt { get; set; }
    }
}
