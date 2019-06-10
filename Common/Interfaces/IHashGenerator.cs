using Models;

namespace Common.Interfaces
{
    public interface IHashGenerator
    {
        string GenerateHashFromBlock(BlockData blockData, string previousBlockHash);
        string GenerateHashRfc2898(string input, byte[] salt);
        byte[] CreateSalt();
    }
}