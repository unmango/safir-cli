using System.Collections.Generic;
using Cli.Internal.Wrappers.Process;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Cli.Services.Dotnet
{
    internal class DotnetRunService : DotnetService
    {
        public DotnetRunService(
            IProcessFactory processFactory,
            IOptions<ConfigOptions> config,
            ILogger<DotnetRunService> logger,
            IEnumerable<string> args)
            : base(processFactory, config, logger, DotnetCommand.Run, args)
        { }
    }
}
