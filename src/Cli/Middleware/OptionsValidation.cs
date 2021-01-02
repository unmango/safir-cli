using System;
using System.CommandLine.Builder;
using System.CommandLine.Invocation;
using System.CommandLine.IO;
using Microsoft.Extensions.Options;

namespace Cli.Middleware
{
    internal static class OptionsValidation
    {
        public static CommandLineBuilder HandleOptionsValidation(this CommandLineBuilder builder)
            => builder.UseMiddleware(async (context, next) => {
                try
                {
                    await next(context);
                }
                catch (Exception e) when (e.InnerException is OptionsValidationException validationException)
                {
                    var message = $"{validationException.OptionsType.Name}: {validationException.Message}";
                    context.Console.Error.WriteLine(message);
                }
            }, MiddlewareOrder.ExceptionHandler);
    }
}
