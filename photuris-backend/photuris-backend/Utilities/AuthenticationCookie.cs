namespace photuris_backend.Utilities
{
    public class AuthenticationCookie
    {
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
