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

            return BitConverter.ToString(byteArray).Replace("-", "");
        }

        public static bool IsNullOrEmpty(this string? input)
        {
            return string.IsNullOrEmpty(input);
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
