using System.CommandLine.Help;
using System.CommandLine.Invocation;
using System.Threading.Tasks;

namespace Cli.Commands
{
    internal sealed class ShowHelpHandler : ICommandHandler
    {
        public IHelpBuilder HelpBuilder { get; set; } = null!;

        public Task<int> InvokeAsync(InvocationContext context)
        {
            HelpBuilder.Write(context.ParseResult.CommandResult.Command);
                
            return Task.FromResult(1);
        }
    }
}
