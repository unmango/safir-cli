using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Help;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using System.Linq;

namespace Cli.Middleware
{
    internal static class HelpMiddleware
    {
        public static CommandLineBuilder UseHelpForEmptyCommands(this CommandLineBuilder builder) =>
            builder.UseMiddleware((context, next) => {
                var commandResult = context.ParseResult.CommandResult;
                
                if (HasChildrenDefined(commandResult.Command) &&
                    !WasPassedChildren(commandResult))
                {
                    return CommandHandler.Create((IHelpBuilder help) => {
                        help.Write(commandResult.Command);
                    }).InvokeAsync(context);
                }

                return next(context);
            });

        private static bool HasChildrenDefined(ISymbol command)
        {
            var children = command.Children.AsEnumerable();

            if (command is Command concrete)
            {
                children = children.Except(concrete.GlobalOptions);
            }

            return children.Any();
        }

        private static bool WasPassedChildren(SymbolResult result)
        {
            return result.Children.Count > 0;
        }
    }
}
