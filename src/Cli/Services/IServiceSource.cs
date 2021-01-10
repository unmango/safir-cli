using Cli.Services.Configuration;

namespace Cli.Services
{
    internal interface IServiceSource
    {
        SourceType? Type { get; }
        
        CommandType? Command { get; }
        
        string? Name { get; }
        
        int? Priority { get; }
    }
}
