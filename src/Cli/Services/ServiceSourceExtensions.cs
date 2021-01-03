using System;
using System.Collections.Generic;
using System.Linq;
using Cli.Services.Installers;

namespace Cli.Services
{
    internal static class ServiceSourceExtensions
    {
        public static IServiceInstaller GetInstaller(this ServiceSource source)
            => source.Type switch {
                SourceType.Git => GetGitInstaller(source),
                SourceType.DotnetTool => GetDotnetToolInstaller(source),
                SourceType.LocalDirectory => GetLocalDirectoryInstaller(source),
                null => throw new NotSupportedException("SourceType must be set to retrieve installer"),
                _ => throw new NotSupportedException("SourceType is not supported")
            };

        public static IServiceInstaller GetGitInstaller(this ServiceSource source)
        {
            if (source.Type != SourceType.Git) throw new ArgumentException("Invalid SourceType");
            if (string.IsNullOrWhiteSpace(source.CloneUrl))
                throw new NotSupportedException("GitCloneUrl must have a value");

            return new GitInstaller(source.CloneUrl);
        }

        public static IServiceInstaller GetDotnetToolInstaller(this ServiceSource source)
        {
            if (source.Type != SourceType.DotnetTool) throw new ArgumentException("Invalid SourceType");
            if (string.IsNullOrWhiteSpace(source.ToolName))
                throw new NotSupportedException("ToolName must have a value");

            return new DotnetToolInstaller(source.ToolName, source.ExtraArgs);
        }

        public static IServiceInstaller GetLocalDirectoryInstaller(this ServiceSource source)
        {
            if (source.Type != SourceType.LocalDirectory) throw new ArgumentException("Invalid SourceType");

            return NoOpInstaller.Value;
        }

        public static IEnumerable<ServiceSource> OrderByPriority(this IEnumerable<ServiceSource> sources)
        {
            return sources.OrderBy(x => x.Priority);
        }

        public static ServiceSource HighestPriority(
            this IEnumerable<ServiceSource> sources,
            Func<ServiceSource, bool>? predicate = null)
        {
            var ordered = sources.OrderByPriority();
            return predicate == null
                ? ordered.First()
                : ordered.First(predicate);
        }

        public static ServiceSource? HighestPriorityOrDefault(
            this IEnumerable<ServiceSource> sources,
            Func<ServiceSource, bool>? predicate = null)
        {
            var ordered = sources.OrderByPriority();
            return predicate == null
                ? ordered.FirstOrDefault()
                : ordered.FirstOrDefault(predicate);
        }
    }
}
