using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace Cli.Services
{
    internal class DotnetRunService : DotnetService
    {
        public DotnetRunService(IOptions<Config> config, IEnumerable<string> args)
            : base(config, "run", args)
        { }
    }
}
