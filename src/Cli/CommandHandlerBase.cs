using System.CommandLine.Invocation;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cli
{
    internal abstract class CommandHandlerBase : ICommandHandler
    {
        public Task<int> InvokeAsync(InvocationContext context)
        {
            var method = GetType().GetMethods().FirstOrDefault(LikeExecute);

            return method == null
                ? Task.FromResult(0)
                : CommandHandler.Create(method, this).InvokeAsync(context);
        }

        private static bool LikeExecute(MethodInfo method)
        {
            return Regex.Match(method.Name, "Execute*").Success;
        }
    }
}
