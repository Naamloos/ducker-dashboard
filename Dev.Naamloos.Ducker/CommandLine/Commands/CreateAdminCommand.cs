

using Dev.Naamloos.Ducker.CommandLine.Attributes;
using Dev.Naamloos.Ducker.Database;
using Dev.Naamloos.Ducker.Database.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Dev.Naamloos.Ducker.CommandLine.Commands
{
    [CliCommand("create-admin", "Creates an admin user.")]
    public class CreateAdminCommand : BaseCommand
    {
        public CreateAdminCommand(IServiceProvider serviceCollection) 
            : base(serviceCollection)
        {
        }

        [CliCommandHandler]
        public async Task ExecuteAsync(string username, string password)
        {
            // Get required services
            var appDbContext = this.Services.GetRequiredService<AppDbContext>();
            var logger = this.Services.GetRequiredService<ILogger<CreateAdminCommand>>();

            // Hash password
            var hasher = new PasswordHasher<object>();
            var hashedPassword = hasher.HashPassword(null, password);

            appDbContext.Users.Add(new User()
            {
                UserName = username,
                PasswordHash = hashedPassword,
                EmailConfirmed = true,
                NormalizedUserName = username.ToUpperInvariant()
            });
            await appDbContext.SaveChangesAsync();

            logger.LogInformation("Creating admin user: {username}", username);
        }
    }
}
