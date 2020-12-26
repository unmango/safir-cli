using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace Cli.Services
{
    internal abstract class DotnetService : ProcessService
    {
        // Lots TODO
        protected DotnetService(IOptions<Config> config, object dotnetCommand, IEnumerable<string> args)
            : base(config, dotnetCommand.ToString() ?? string.Empty, args)
        { }
    }
}
