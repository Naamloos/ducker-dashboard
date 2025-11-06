using Dev.Naamloos.Ducker.Database;

namespace Dev.Naamloos.Ducker.Seeders
{
    public interface ISeeder
    {
        Task SeedAsync(IServiceProvider serviceProvider);
    }
}
