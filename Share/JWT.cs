namespace Share
{
    public class JWT
    {
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string SecretKey { get; set; } = string.Empty;
        public double ExpirationInMinutes { get; set; }
    }
}