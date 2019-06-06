using System.Collections.Generic;
using Models;
using Models.AddingThread;

namespace Repository
{
    public static class Datas
    {
        public static IEnumerable<Pocket> Pockets { get; set; }
        public static Block GenesisBlock { get; set; }
        public static byte[] Salt { get; set; }
}
}
