using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Cli.Services
{
    internal class DotnetRunService : DotnetService
    {
        public DotnetRunService(
            IProcessFactory processFactory,
            IOptions<Config> config,
            ILogger<DotnetRunService> logger,
            IEnumerable<string> args)
            : base(processFactory, config, logger, "run", args)
        { }
    }
}
