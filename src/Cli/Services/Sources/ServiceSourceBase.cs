using Cli.Services.Configuration;

namespace Cli.Services.Sources
{
    internal abstract record ServiceSourceBase : IServiceSource
    {
        public SourceType? Type { get; init; }
        
        public CommandType? Command { get; init; }
        
        public string? Name { get; init; }
        
        public int? Priority { get; init; }
    }
}
