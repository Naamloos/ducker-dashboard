namespace Dev.Naamloos.Ducker.Database.Entities
{
    public class RefreshToken
    {
        public ulong Id { get; set; }
        public string Token { get; set; } = default!;
        public DateTimeOffset ExpiresAt { get; set; }
        public DateTimeOffset? RevokedAt { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        public string UserId { get; set; } = default!;
        public User User { get; set; } = default!;
        public bool IsActive => RevokedAt == null && DateTimeOffset.UtcNow >= ExpiresAt;
    }
}
