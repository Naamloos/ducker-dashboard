using System.ComponentModel.DataAnnotations.Schema;

namespace Dev.Naamloos.Ducker.Database.Entities
{
    public class TeamMembership
    {
        [ForeignKey(nameof(Team))]
        public ulong TeamId { get; set; }

        public Team Team { get; set; } = default!;

        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = default!;

        public User User { get; set; } = default!;
    }
}
