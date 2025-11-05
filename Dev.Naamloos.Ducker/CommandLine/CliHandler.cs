using Dev.Naamloos.Ducker.CommandLine.Services;
using Dev.Naamloos.Ducker.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Dev.Naamloos.Ducker.CommandLine
{
    public class CliHandler : IDisposable
    {
        private readonly string[] _args;
        private IHost? _host;

        public CliHandler(string[] args)
        {
            _args = args;
        }

        /// <summary>
        /// Handles the command line arguments.
        /// </summary>
        /// <returns>true if the app should exit after.</returns>
        public bool Handle()
        {
            if (_args.Length == 0)
            {
                return false;
            }

            _host = Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    // Inject potential services needed for CLI Host
                    services.AddDbContext<AppDbContext>(opts =>
                    {
                        opts.UseSqlite("Data Source=app.db");
                    }, ServiceLifetime.Singleton, ServiceLifetime.Singleton);
                    // Inject logger
                    services.AddLogging();
                    // Inject relevant data for CLI Host
                    services.AddSingleton<CliData>(x => new CliData() { Args = _args });
                    // Inject CLI Host
                    services.AddHostedService<CliHost>();
                })
                .Build();

            _host.Run();

            return true;
        }

        public void Dispose()
        {
            _host?.Dispose();
        }
    }
}
