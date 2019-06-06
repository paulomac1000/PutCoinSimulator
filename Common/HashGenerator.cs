using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Common.Interfaces;
using Models;
using Repository;

namespace Common
{
    public class HashGenerator : IHashGenerator
    {
        public string GenerateHashFromBlock(BlockData blockData, string previousBlockHash)
        {
            var serialized = new KeyValuePair<BlockData, string>(blockData, previousBlockHash).Serialize();
            if (Datas.Salt == null)
                Datas.Salt = CreateSalt();

            var hash = GenerateHash(serialized, Datas.Salt);
            return hash;
        }

        public string GenerateHash(string input, byte[] salt)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(input, salt, Settings.HashingIterations);
            var hash = pbkdf2.GetBytes(20);
            var hashBytes = new byte[36];

            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            var hashedInput = Convert.ToBase64String(hashBytes);
            return hashedInput;
        }

        public byte[] CreateSalt()
        {
            byte[] salt;
            new RNGCryptoServiceProvider(salt = new byte[16]);
            return salt;
        }
    }
}
