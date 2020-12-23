using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Invocation;
using System.CommandLine.IO;
using System.Linq;
using Cli.Commands;

namespace Cli.Middleware
{
    internal static class ServiceMiddleware
    {
        public static CommandLineBuilder UseRequireServiceName(this CommandLineBuilder builder) => builder
            .UseMiddleware((context, next) => {
                if (context.ParseResult.CommandResult.Parent?.Symbol is ServiceCommand
                    && !context.ParseResult.CommandResult.Command.Options.Any())
                {
                    return CommandHandler.Create((IConsole console) => {
                        console.Error.WriteLine("Service name is required");
                    }).InvokeAsync(context);
                }

                return next(context);
            });
    }
}
