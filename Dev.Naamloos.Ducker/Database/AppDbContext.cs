using Dev.Naamloos.Ducker.Database.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dev.Naamloos.Ducker.Database
{
    public class AppDbContext : IdentityDbContext<User, Role, string>
    {
        public DbSet<RefreshToken> SessionTokens { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamMembership> TeamMemberships { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Composite keys that can not be expressed using attributes
            builder.Entity<TeamMembership>()
                .HasKey(tm => new { tm.UserId, tm.TeamId });

            base.OnModelCreating(builder);
        }
    }
}
