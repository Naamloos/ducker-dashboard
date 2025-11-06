namespace Dev.Naamloos.Ducker.CommandLine.Commands
{
    public abstract class BaseCommand
    {
        protected IServiceProvider Services { get; }

        public BaseCommand(IServiceProvider services)
        {
            this.Services = services;
        }
    }
}
