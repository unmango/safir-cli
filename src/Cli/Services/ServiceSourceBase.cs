namespace Cli.Services
{
    internal abstract record ServiceSourceBase : IServiceSource
    {
        public SourceType? Type { get; init; }
        
        public CommandType? Command { get; init; }
        
        public string? Name { get; init; }
        
        public int? Priority { get; init; }
    }
}
