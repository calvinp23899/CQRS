using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Extensions
{
    public static class CommonExtension
    {
        private static readonly string Key = "SecretEncryptionKey123@";

        public static string EncryptData(this string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Value cannot be null or empty", nameof(value));

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            byte[] keyBytes = Encoding.UTF8.GetBytes(Key);

            byte[] encryptedBytes = new byte[valueBytes.Length];

            for (int i = 0; i < valueBytes.Length; i++)
            {
                encryptedBytes[i] = (byte)(valueBytes[i] ^ keyBytes[i % keyBytes.Length]);
            }

            return Convert.ToBase64String(encryptedBytes);
        }

        public static string DecryptData(this string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Value cannot be null or empty", nameof(value));

            byte[] encryptedBytes = Convert.FromBase64String(value);
            byte[] keyBytes = Encoding.UTF8.GetBytes(Key);

            byte[] decryptedBytes = new byte[encryptedBytes.Length];

            for (int i = 0; i < encryptedBytes.Length; i++)
            {
                decryptedBytes[i] = (byte)(encryptedBytes[i] ^ keyBytes[i % keyBytes.Length]);
            }

            return Encoding.UTF8.GetString(decryptedBytes);
        }
    }
}
