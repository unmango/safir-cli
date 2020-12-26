using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace Cli.Services
{
    internal class DotnetToolService : DotnetService
    {
        public DotnetToolService(IOptions<Config> config, IEnumerable<string> args)
            : base(config, "tool", args)
        { }
    }
}
