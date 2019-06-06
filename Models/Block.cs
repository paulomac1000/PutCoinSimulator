namespace Models
{
    public class Block
    {
        public string Hash { get; set;  }
        public BlockData Data { get; set; }
        public string PreviousBlockHash { get; set; }
    }
}
