using System.Security.Cryptography;
using System.Text;

namespace photuris_backend.Extensions
{
    public static class StringExtensions
    {
        private static readonly Random Random = new Random();
        private const string AllowedChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        public static string ApplySha256(this string input)
        {
            byte[] byteArray;
            using (var hashAlgorithm = SHA256.Create())
            {
                byteArray = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));
            }

            var stringBuilder = new StringBuilder();
            foreach (var _byte in byteArray)
            {
                stringBuilder.Append(_byte.ToString("X2"));
            }

            return stringBuilder.ToString();
        }

        public static string GetRandom(int length)
        {
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
                chars[i] = AllowedChars[Random.Next(0, AllowedChars.Length)];
            return new string(chars);
        }
    }
}
