using System.Collections.Generic;

namespace Models
{
    /// <summary>
    /// Data about transaction used in block
    /// </summary>
    public class BlockData
    {
        public IEnumerable<Transaction> Transactions { get; set; }
    }
}
