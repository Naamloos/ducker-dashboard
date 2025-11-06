

using Dev.Naamloos.Ducker.CommandLine.Attributes;
using Dev.Naamloos.Ducker.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Dev.Naamloos.Ducker.CommandLine.Commands
{
    [CliCommand("migrate-rollback", "Rolls back the last database migration.")]
    public class MigrateRollbackCommand : BaseCommand
    {
        public MigrateRollbackCommand(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        [CliCommandHandler]
        public async Task ExecuteAsync()
        {
            // Get required services
            var appDbContext = this.Services.GetRequiredService<AppDbContext>();
            var logger = this.Services.GetRequiredService<ILogger<MigrateRollbackCommand>>();

            logger.LogInformation("Rolling back the last database migration...");
            var lastMigration = (await appDbContext.Database.GetAppliedMigrationsAsync())
                .LastOrDefault();
            if (lastMigration != null)
            {
                var migrations = await appDbContext.Database.GetAppliedMigrationsAsync();
                var targetMigration = migrations
                    .Reverse()
                    .SkipWhile(m => m != lastMigration)
                    .Skip(1)
                    .FirstOrDefault();
                await appDbContext.Database.MigrateAsync(targetMigration);
                await appDbContext.SaveChangesAsync();
                logger.LogInformation("Rolled back migration: {Migration}", lastMigration);
            }
            else
            {
                logger.LogInformation("No migrations to roll back.");
            }
        }
    }
}
