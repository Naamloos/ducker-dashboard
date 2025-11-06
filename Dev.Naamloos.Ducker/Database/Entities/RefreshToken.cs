using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dev.Naamloos.Ducker.Database.Entities
{
    [Index(nameof(Token), IsUnique = true)]
    public class RefreshToken
    {
        [Key]
        public ulong Id { get; set; }

        public string Token { get; set; } = default!;

        public DateTimeOffset ExpiresAt { get; set; }

        public DateTimeOffset? RevokedAt { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = default!;

        public User User { get; set; } = default!;

        public bool IsActive => RevokedAt == null && DateTimeOffset.UtcNow >= ExpiresAt;
    }
}
