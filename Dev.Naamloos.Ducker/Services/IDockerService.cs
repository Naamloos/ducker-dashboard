using Dev.Naamloos.Ducker.Entities;

namespace Dev.Naamloos.Ducker.Services
{
    public interface IDockerService
    {
        public Task<IEnumerable<Container>> ListContainersAsync();
    }
}
