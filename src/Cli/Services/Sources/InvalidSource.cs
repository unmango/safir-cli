using System.Collections.Generic;
using Cli.Services.Configuration;

namespace Cli.Services.Sources
{
    internal record InvalidSource(
        ServiceSource Source,
        IEnumerable<string> Errors) : ServiceSource
    {
        public InvalidSource(ServiceSource s, params string[] errors)
            : this(s, (IEnumerable<string>)errors)
        {
        }
    }
}
