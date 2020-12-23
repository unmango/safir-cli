using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using Cli.Commands.Service;
using Microsoft.Extensions.Hosting;

namespace Cli.Commands
{
    internal sealed class ServiceCommand : Command
    {
        public ServiceCommand() : base("service", "Control various Safir services")
        {
            AddAlias("s");
            AddCommand(new RestartCommand());
            AddCommand(new StartCommand());
        }
    }

    internal static class ServiceCommandExtensions
    {
        public static T AddServiceCommand<T>(this T builder)
            where T : CommandLineBuilder => builder
            .AddCommand(new ServiceCommand())
            .AddCommand(new StartCommand());

        public static IHostBuilder AddServiceCommand(this IHostBuilder builder) => builder
            .UseCommandHandler<RestartCommand, RestartCommand.RestartHandler>()
            .UseCommandHandler<StartCommand, StartCommand.StartHandler>();
    }
}
