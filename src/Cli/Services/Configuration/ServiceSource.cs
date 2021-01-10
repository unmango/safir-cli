using System;
using Cli.Services.Sources;

namespace Cli.Services.Configuration
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal record ServiceSource : ServiceSourceBase,
        IDockerBuildSource,
        IDockerImageSource,
        IDotnetToolSource,
        IGitSource,
        ILocalDirectorySource
    {
        public string? BuildContext { get; init; }
        
        public string? CloneUrl { get; init; }
        
        public string? ExtraArgs { get; init; }
        
        public string? ImageName { get; init; }
        
        public string? ProjectFile { get; init; }
        
        public string? SourceDirectory { get; init; }
        
        public string? Tag { get; init; }
        
        public string? ToolName { get; init; }

        string IDockerBuildSource.BuildContext
            => BuildContext ?? throw new InvalidOperationException(NullCast(nameof(BuildContext)));

        string IGitSource.CloneUrl => CloneUrl ?? throw new InvalidOperationException(NullCast(nameof(CloneUrl)));

        string IDockerImageSource.ImageName
            => ImageName ?? throw new InvalidOperationException(NullCast(nameof(ImageName)));

        string ILocalDirectorySource.SourceDirectory
            => SourceDirectory ?? throw new InvalidOperationException(NullCast(nameof(SourceDirectory)));

        string IDotnetToolSource.ToolName
            => ToolName ?? throw new InvalidOperationException(NullCast(nameof(ToolName)));

        private static string NullCast(string propName) => $"{propName} was null";
    }
}
