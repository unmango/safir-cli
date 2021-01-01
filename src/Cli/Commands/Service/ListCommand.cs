using System.CommandLine;
using System.CommandLine.Invocation;
using System.Threading.Tasks;

namespace Cli.Commands.Service
{
    public sealed class ListCommand : Command
    {
        public ListCommand() : base("list", "List services")
        {
            AddAlias("ls");
        }

        public sealed class ListHandler : ICommandHandler
        {
            public Task<int> InvokeAsync(InvocationContext context)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
