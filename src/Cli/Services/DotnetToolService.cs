using System.Collections.Generic;
using Cli.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Cli.Services
{
    internal class DotnetToolService : DotnetService
    {
        public DotnetToolService(
            IProcessFactory processFactory,
            IOptions<Config> config,
            ILogger<DotnetToolService> logger,
            IEnumerable<string> args)
            : base(processFactory, config, logger, "tool", args)
        { }
    }
}
