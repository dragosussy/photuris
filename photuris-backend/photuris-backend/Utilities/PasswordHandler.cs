using photuris_backend.Extensions;

namespace photuris_backend.Utilities
{
    public static class PasswordHandler
    {
        public static string EncryptPasswordSha256(string salt, string? password)
        {
            return (salt + password).ApplySha256();
        }

        public static string GenerateSalt(int length = 5)
        {
            return StringExtensions.GetRandom(length);
        }
    }
}
