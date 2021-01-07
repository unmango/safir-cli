using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cli.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Cli.Internal
{
    internal class ConfigurationServiceDirectory : IServiceDirectory
    {
        private readonly IOptions<CliOptions> _options;
        private readonly ILogger<ConfigurationServiceDirectory> _logger;

        public ConfigurationServiceDirectory(
            IOptions<CliOptions> options,
            ILogger<ConfigurationServiceDirectory> logger)
        {
            _options = options;
            _logger = logger;
        }

        public string GetInstallationDirectory(IEnumerable<string>? extraPaths = null)
        {
            List<string> extraDirs = new();
            List<string> extraParts = new();

            if (extraPaths != null)
            {
                var filtered = extraPaths.Where(x => !string.IsNullOrWhiteSpace(x));
                var directories = filtered.Where(Path.EndsInDirectorySeparator).ToList();
                var rooted = directories.Where(Path.IsPathRooted).ToList();

                // If passed a valid rooted path, return the first that exists 
                if (rooted.Count >= 1) return rooted.First(Directory.Exists);

                var relativeDirs = directories.ToLookup(x => x.Contains(Path.DirectorySeparatorChar));
                extraDirs.AddRange(relativeDirs[true]);
                extraParts.AddRange(relativeDirs[false]);
            }

            throw new System.NotImplementedException();
        }

        private static bool ValidRootedPath(string path) =>
            Path.IsPathRooted(path) && Path.EndsInDirectorySeparator(path);
    }
}
