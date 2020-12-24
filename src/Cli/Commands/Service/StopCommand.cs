using System.CommandLine;
using System.CommandLine.Invocation;
using System.Threading.Tasks;

namespace Cli.Commands.Service
{
    internal sealed class StopCommand : Command
    {
        public StopCommand() : base("stop", "Stop the selected service")
        {
            AddOption(ServiceOption.Value);
        }
        
        public sealed class StopHandler : ICommandHandler
        {
            public Task<int> InvokeAsync(InvocationContext context)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
