using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Cli.Services.Configuration;

namespace Cli.Services.Installation
{
    internal record InstallationContext(
        string WorkingDirectory,
        ServiceEntry Service,
        IEnumerable<ServiceSource> Sources)
    {
        public Exception? Exception { get; init; }
        
        public bool Installed { get; init; }
        
        public IImmutableDictionary<object, object> Properties { get; init; } =
            ImmutableDictionary<object, object>.Empty;
    }
}
