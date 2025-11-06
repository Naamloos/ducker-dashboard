

using Dev.Naamloos.Ducker.CommandLine.Attributes;
using Dev.Naamloos.Ducker.Database;
using Microsoft.EntityFrameworkCore;

namespace Dev.Naamloos.Ducker.CommandLine.Commands
{
    [CliCommand("migrate", "Applies all pending migrations")]
    public class MigrateCommand : BaseCommand
    {
        public MigrateCommand(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        [CliCommandHandler]
        public async Task ExecuteAsync()
        {
            // Get required services
            var appDbContext = this.Services.GetRequiredService<AppDbContext>();
            var logger = this.Services.GetRequiredService<ILogger<MigrateCommand>>();

            logger.LogInformation("Running database migrations...");
            await appDbContext.Database.MigrateAsync();
            logger.LogInformation("Database migrations completed.");
        }
    }
}
