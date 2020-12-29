using System.CommandLine;
using System.CommandLine.Invocation;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Cli.Commands.Service
{
    internal sealed class StartCommand : Command
    {
        public StartCommand() : base("start", "Start the selected service(s)")
        {
            AddOption(new ServiceOption());
        }
        
        // ReSharper disable once ClassNeverInstantiated.Global
        public sealed class StartHandler : ICommandHandler
        {
            private readonly IOptions<Options> _options;

            public StartHandler(IOptions<Options> options)
            {
                _options = options;
            }
            
            public Task<int> InvokeAsync(InvocationContext context)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
