using Dev.Naamloos.Ducker.Database;
using Dev.Naamloos.Ducker.Database.Entities;
using Dev.Naamloos.Ducker.Middleware;
using Dev.Naamloos.Ducker.Services;
using InertiaCore.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Dev.Naamloos.Ducker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // ensure data directory exists
            // TODO interpret location from env and set as working directory.
            if (!Directory.Exists("data"))
            {
                Directory.CreateDirectory("data");
            }

            var builder = WebApplication.CreateBuilder(args);

            // TODO: Path configurable via environment or config file
            builder.Services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlite("Data Source=data/app.db");
            });

            builder.Services.AddIdentity<User, Role>(opts =>
            {
                opts.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            // Cookie auth for regular users
            builder.Services.AddAuthentication()
                .AddJwtBearer("api", opts =>
                {
                    var key = Encoding.UTF8.GetBytes("test");
                    opts.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "Ducker",
                        ValidAudience = "DuckerAPI",
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            builder.Services.ConfigureApplicationCookie(opts =>
            {
                opts.LoginPath = "/auth/login";
                opts.LogoutPath = "/auth/logout";
                opts.Cookie.HttpOnly = true;
                opts.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                opts.SlidingExpiration = true;
                opts.ExpireTimeSpan = TimeSpan.FromDays(1);
            });

            builder.Services.AddAuthorization();

            builder.Services.AddInertia(x =>
            {
                x.RootView = "~/Views/App.cshtml";
            });
            builder.Services.AddViteHelper(x =>
            {
                x.PublicDirectory = "wwwroot";
                x.BuildDirectory = "build";
                x.ManifestFilename = "manifest.json";
            });
            builder.Services.AddControllersWithViews()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.DictionaryKeyPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
                });

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddSingleton<IDockerService, DockerService>();

            var app = builder.Build();

            app.UseInertia();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.UseStaticFiles();

            app.UseMiddleware<InertiaPropsMiddleware>();

            // This check prevents ef core tools breaking
            if (!args.Any(x => x.Contains("--applicationName")) && args.Length > 0)
            {
                // Create a service scope and pass to CLI handler
                using var scope = app.Services.CreateScope();
                var cli = new CommandLine.Cli(scope, args);
                Task.Run(() => cli.HandleAsync()).Wait();

                // Exit if CLI handled the arguments.
                // If not, the app should start as normal.
                #if DEBUG
                Console.ReadLine();
                #endif
                return;
            }

            app.Run();
        }
    }
}
