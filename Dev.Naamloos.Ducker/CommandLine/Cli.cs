using Dev.Naamloos.Ducker.CommandLine.Commands;
using Dev.Naamloos.Ducker.Database;
using Dev.Naamloos.Ducker.Database.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Dev.Naamloos.Ducker.CommandLine
{
    public class Cli
    {
        private readonly IServiceScope _serviceScope;
        private readonly string[] _args;

        public Cli(IServiceScope serviceScope, string[] args)
        {
            _serviceScope = serviceScope;
            _args = args;
        }

        public async Task HandleAsync()
        {
            var logger = _serviceScope.ServiceProvider.GetRequiredService<ILogger<Cli>>();
            var command = _args[0];
            var arguments = _args.Skip(1).ToArray();

            // Gather all available commands
            var commands = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(BaseCommand)) && !t.IsAbstract)
                .Where(t => t.GetCustomAttribute<Attributes.CliCommandAttribute>() is not null)
                .ToList();

            // Find the requested command
            var cliCommandType = commands
                .FirstOrDefault(t => t.GetCustomAttribute<Attributes.CliCommandAttribute>()!.Name == command);

            if (cliCommandType == default)
            {
                logger.LogError("Unknown command: {command}", command);
                return;
            }

            // Check if command has a constructor that accepts ONLY IServiceProvider
            var constructor = cliCommandType.GetConstructor(new Type[] { typeof(IServiceProvider) });
            if (constructor == null)
            {
                logger.LogError("Command {command} does not have a valid constructor.", command);
                return;
            }

            // Check if there's only one handler method
            var handlerMethodCount = cliCommandType
                .GetMethods()
                .Count(m => m.GetCustomAttribute<Attributes.CliCommandHandlerAttribute>() is not null);

            if (handlerMethodCount != 1)
            {
                logger.LogError("Command {command} must have exactly one handler method.", command);
                return;
            }

            // Find the handler method marked with [CliCommandHandler]
            var handlerMethod = cliCommandType
                .GetMethods()
                .FirstOrDefault(m => m.GetCustomAttribute<Attributes.CliCommandHandlerAttribute>() is not null);

            if (handlerMethod == default)
            {
                logger.LogError("No handler method found for command: {command}", command);
                return;
            }

            // Check if all arguments of handler are strings
            var handlerParameters = handlerMethod.GetParameters();
            if (handlerParameters.Any(p => p.ParameterType != typeof(string)))
            {
                logger.LogError("Handler method for command {command} has non-string parameters.", command);
                return;
            }

            // Check argument count against handler parameters
            if (arguments.Length != handlerParameters.Length)
            {
                logger.LogError("Invalid argument count for command {command}. Expected {expected}, got {actual}.", 
                    command, 
                    handlerParameters.Length, 
                    arguments.Length);

                logger.LogInformation("Usage: {command} {arguments}", 
                    command, 
                    string.Join(" ", handlerParameters.Select(p => $"<{p.Name}>"))
                );
                return;
            }

            // Our command is found and valid and the argument count is correct. Time to execute it.
            var cliCommand = Activator.CreateInstance(cliCommandType, _serviceScope.ServiceProvider);
            if (cliCommand is null) {
                logger.LogError("Failed to create instance of command: {command}", command);
                return;
            }

            var executableTask = handlerMethod.Invoke(cliCommand!, arguments);
            if(executableTask is Task task)
            {
                await task;
            }

            _serviceScope.Dispose();
        }
    }
}
