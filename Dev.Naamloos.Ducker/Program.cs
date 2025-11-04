using Dev.Naamloos.Ducker.Services;
using InertiaCore.Extensions;

namespace Dev.Naamloos.Ducker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Services.AddInertia(x =>
            {
                x.RootView = "~/Views/App.cshtml";
            });
            builder.Services.AddViteHelper(x =>
            {
                x.PublicDirectory = "wwwroot";
                x.BuildDirectory = "dist";
                x.ManifestFilename = "manifest.json";
            });
            builder.Services.AddControllersWithViews();

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

            app.UseAuthorization();

            app.MapControllers();

            app.UseStaticFiles();

            app.Run();
        }
    }
}
