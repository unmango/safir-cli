using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using System.CommandLine.Invocation;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Cli.Commands
{
    internal sealed class ServiceCommand : Command
    {
        public ServiceCommand() : base("service", "Control various Safir services")
        {
            AddAlias("s");
        }
    }

    internal sealed class ServiceHandler : CommandHandlerBase
    {
        protected override ICommandHandler GetHandler() => Create(Execute);

        private void Execute()
        {
            
        }
    }

    internal static class ServiceCommandExtensions
    {
        public static T AddServiceCommand<T>(this T builder)
            where T : CommandLineBuilder
        {
            return builder.AddCommand(new ServiceCommand());
        }

        public static IHostBuilder AddServiceCommand(this IHostBuilder builder)
        {
            return builder.UseCommandHandler<ServiceCommand, ServiceHandler>();
        }
    }
}
