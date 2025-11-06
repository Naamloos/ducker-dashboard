
using Dev.Naamloos.Ducker.CommandLine.Attributes;
using Dev.Naamloos.Ducker.Seeders;
using System.Reflection;

namespace Dev.Naamloos.Ducker.CommandLine.Commands
{
    [CliCommand("seed", "Seeds the database with initial data.")]
    public class SeedCommand : BaseCommand
    {
        public SeedCommand(IServiceProvider services) : base(services)
        {
        }

        [CliCommandHandler]
        public async Task ExecuteAsync(string seederName)
        {
            // Get required services
            var appDbContext = this.Services.GetRequiredService<Database.AppDbContext>();
            var logger = this.Services.GetRequiredService<ILogger<SeedCommand>>();
            logger.LogInformation("Seeding the database with initial data...");
            Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsAssignableTo(typeof(ISeeder)) && !t.IsInterface && !t.IsAbstract)
                .ToList()
                .ForEach(async seederType =>
                {
                    if (string.Equals(seederType.Name, seederName, StringComparison.OrdinalIgnoreCase))
                    {
                        var seeder = (ISeeder)Activator.CreateInstance(seederType)!;
                        await seeder.SeedAsync(Services);
                        logger.LogInformation("Seeder {Seeder} executed successfully.", seederName);
                    }
                });
        }
    }
}
