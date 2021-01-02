// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using System.Collections.Generic;
using Cli.Services;

namespace Cli
{
    internal record CliOptions
    {
        public ConfigOptions Config { get; init; } = new();

        public ServiceOptions Services { get; init; } = new();
    }

    internal record ConfigOptions
    {
        public string Directory { get; init; } = string.Empty;

        public bool Exists { get; init; }
        
        public string File { get; init; } = string.Empty;
    }

    internal class ServiceOptions : Dictionary<string, ServiceEntry>
    {
        public const string DefaultInstallationDirectory = "services";
        
        public string? CustomInstallationDirectory { get; init; }

        public string InstallationDirectory => CustomInstallationDirectory ?? DefaultInstallationDirectory;
    }
}
