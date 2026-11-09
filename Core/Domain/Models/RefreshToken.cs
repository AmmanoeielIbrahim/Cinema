namespace Domain.Models
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public DateTime ExpiresOnUtc { get; set; }

        public User User { get; set; } = default!;
    }
}