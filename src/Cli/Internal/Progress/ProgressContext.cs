using System.Collections.Immutable;

namespace Cli.Internal.Progress
{
    internal record ProgressContext<T>(T Value)
    {
        public IImmutableDictionary<object, object> Properties { get; } = ImmutableDictionary<object, object>.Empty;
        
        public ProgressScope Scope { get; init; } = ProgressScope.Empty;
    }
}
