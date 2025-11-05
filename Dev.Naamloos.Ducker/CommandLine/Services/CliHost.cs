
using Dev.Naamloos.Ducker.Database;
using Dev.Naamloos.Ducker.Database.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Dev.Naamloos.Ducker.CommandLine.Services
{
    public class CliHost : IHostedService
    {
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private readonly ILogger<CliHost> _logger;
        private readonly CliData _data;
        private readonly AppDbContext _appDbContext;

        public CliHost(IHostApplicationLifetime hostApplicationLifetime, ILogger<CliHost> logger, CliData data, AppDbContext appDbContext)
        {
            _hostApplicationLifetime = hostApplicationLifetime;
            _logger = logger;
            _data = data;
            _appDbContext = appDbContext;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            switch(_data.Args[0])
            {
                case "migrate":
                    _logger.LogInformation("Running database migrations...");
                    await _appDbContext.Database.MigrateAsync();
                    _logger.LogInformation("Database migrations completed.");
                    break;
                case "migrate-rollback":
                    _logger.LogInformation("Rolling back the last database migration...");
                    var lastMigration = (await _appDbContext.Database.GetAppliedMigrationsAsync())
                        .LastOrDefault();
                    if (lastMigration != null)
                    {
                        var migrations = await _appDbContext.Database.GetAppliedMigrationsAsync();
                        var targetMigration = migrations
                            .Reverse()
                            .SkipWhile(m => m != lastMigration)
                            .Skip(1)
                            .FirstOrDefault();
                        await _appDbContext.Database.MigrateAsync(targetMigration);
                        await _appDbContext.SaveChangesAsync();
                        _logger.LogInformation("Rolled back migration: {Migration}", lastMigration);
                    }
                    else
                    {
                        _logger.LogInformation("No migrations to roll back.");
                    }
                    break;
                case "create-admin":
                    if(_data.Args.Length < 3)
                    {
                        _logger.LogError("Incorrect argument count");
                        return;
                    }
                    var email = _data.Args[1];
                    var password = _data.Args[2];
                    var hasher = new PasswordHasher<object>();
                    var hash = hasher.HashPassword(null, password);

                    _appDbContext.Users.Add(new User()
                    {
                        UserName = "admin",
                        Email = email,
                        PasswordHash = hash,
                        EmailConfirmed = true,
                        NormalizedEmail = email.ToUpperInvariant(),
                        NormalizedUserName = "ADMIN"
                    });
                    _appDbContext.SaveChanges();
                    _logger.LogInformation("Creating admin user with email: {Email}", email);
                    break;
                default:
                    _logger.LogInformation("Unknown command: {Command}", _data.Args[0]);
                    break;
            }

            _hostApplicationLifetime.StopApplication();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            // stop quietly
        }
    }
}
