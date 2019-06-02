namespace Models
{
    public class Block
    {
        public string Hash { get; set;  }
        public BlockData Data { get; set; }
        public string BlockHash { get; set; }
        public string PreviousBlockHash { get; set; }
    }
}
