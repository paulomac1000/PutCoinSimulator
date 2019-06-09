using System.Collections.Generic;
using System.Linq;
using Models.AddingThread;

namespace Repository
{
    public static class Helpers
    {
        public static double GetAccountBalanceByOwnerName(string ownerName)
        {
            var valueOfIncomingTransactions = Datas.Blockchain.SelectMany(b =>
                    b.Data.Transactions.Where(t => t.Receiver == ownerName).Select(t => t.Amount))
                .Sum();

            var valueOfOutgoingTransactions = Datas.Blockchain.SelectMany(b =>
                    b.Data.Transactions.Where(t => t.Sender == ownerName).Select(t => t.Amount))
                .Sum();

            return valueOfIncomingTransactions - valueOfOutgoingTransactions;
        }

        public static IEnumerable<Pocket> GetPocketWhichHasMoney(double minAccountBalance = 0)
        {
            return Datas.Pockets.Where(p => GetAccountBalanceByOwnerName(p.OwnerName) > minAccountBalance);
        }
    }
}
