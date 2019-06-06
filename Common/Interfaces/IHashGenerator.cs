using Models;

namespace Common.Interfaces
{
    public interface IHashGenerator
    {
        string GenerateHashFromBlock(BlockData blockData, string previousBlockHash);
        string GenerateHash(string input, byte[] salt);
        byte[] CreateSalt();
    }
}