using System.Collections.Generic;
using Cli.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Cli.Services.Dotnet
{
    internal abstract class DotnetService : ProcessService
    {
        // Lots TODO
        protected DotnetService(
            IProcessFactory processFactory,
            IOptions<Config> config,
            ILogger<DotnetService> logger,
            object dotnetCommand,
            IEnumerable<string> args)
            : base(processFactory, config, logger, dotnetCommand.ToString() ?? string.Empty, args)
        { }
    }
}
