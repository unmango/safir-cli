using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Linq;
using System.Threading.Tasks;
using Cli.Internal;
using Microsoft.Extensions.Options;

namespace Cli.Commands.Service
{
    internal class InstallCommand : Command
    {
        private static readonly Option<bool> _concurrent = new(
            new[] { "--concurrent" },
            "Install multiple services concurrently");

        private static readonly Option<string> _directory = new(
            new[] { "-d", "--directory" },
            "Optional directory to install to");

        private static readonly ServiceArgument _services = new("The name of the service to install");
        
        public InstallCommand() : base("install", "Installs the specified service(s)")
        {
            AddOption(_concurrent);
            AddOption(_directory);
            AddArgument(_services);
        }

        // ReSharper disable once ClassNeverInstantiated.Global
        public sealed class InstallHandler : ICommandHandler
        {
            private readonly IOptions<ServiceOptions> _options;
            private readonly IInstallationService _installer;

            public InstallHandler(IOptions<ServiceOptions> options, IInstallationService installer)
            {
                _options = options;
                _installer = installer;
            }
            
            public async Task<int> InvokeAsync(InvocationContext context)
            {
                var parseResult = context.ParseResult;
                var concurrent = parseResult.ValueForOption(_concurrent);
                var directory = parseResult.ValueForOption(_directory);
                var services = parseResult.ValueForArgument(_services);
                
                var toInstall = _options.Value.Join(
                    services!,
                    x => x.Key,
                    x => x,
                    (pair, _) => pair.Value,
                    StringComparer.OrdinalIgnoreCase);

                await _installer.InstallAsync(
                    toInstall,
                    concurrent,
                    directory,
                    context.GetCancellationToken());

                return context.ResultCode;
            }
        }
    }
}
