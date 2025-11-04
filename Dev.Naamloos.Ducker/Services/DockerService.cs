using Dev.Naamloos.Ducker.Entities;
using Docker.DotNet;
using System.Linq;

namespace Dev.Naamloos.Ducker.Services
{
    public class DockerService : IDockerService
    {
        private readonly DockerClient dockerClient;

        public DockerService()
        {
            dockerClient = new DockerClientConfiguration(
                new Uri("npipe://./pipe/docker_engine"))
                .CreateClient();
        }

        public async Task<IEnumerable<Container>> ListContainersAsync()
        {
            var containers = await dockerClient.Containers.ListContainersAsync(
                new Docker.DotNet.Models.ContainersListParameters()
                {
                    All = true
                });
            return containers.Select(x =>
            {
                return new Container
                {
                    Id = x.ID,
                    Name = x.Names != null && x.Names.Count > 0 ? x.Names[0].TrimStart('/') : string.Empty,
                    Status = x.Status,
                    Image = x.Image,
                    ImageId = x.ImageID,
                    Command = x.Command,
                    Created = x.Created.ToString("g"),
                    Labels = x.Labels.ToDictionary(x =>
                    {
                        return x.Key;
                    }, x =>
                    {
                        return x.Value;
                    }),
                    State = x.State
                };
            });
        }
    }
}
