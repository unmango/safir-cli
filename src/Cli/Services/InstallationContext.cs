using System.Collections.Generic;
using System.Collections.Immutable;

namespace Cli.Services
{
    internal record InstallationContext(
        string WorkingDirectory,
        ServiceEntry Service,
        IEnumerable<ServiceSource> Sources)
    {
        private ImmutableDictionary<object, object> Properties { get; init; } =
            ImmutableDictionary<object, object>.Empty;
    }
}
