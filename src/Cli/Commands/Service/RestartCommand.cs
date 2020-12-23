using System.CommandLine;
using System.CommandLine.Invocation;
using System.Threading.Tasks;

namespace Cli.Commands.Service
{
    internal sealed class RestartCommand : Command
    {
        public RestartCommand() : base("restart", "Restart the selected service")
        {
            AddAlias("r");
        }
        
        public sealed class RestartHandler : ICommandHandler
        {
            public Task<int> InvokeAsync(InvocationContext context)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
