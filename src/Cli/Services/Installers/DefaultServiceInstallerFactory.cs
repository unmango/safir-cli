using System;
using System.Collections.Generic;
using System.Linq;

namespace Cli.Services.Installers
{
    internal static class DefaultServiceInstallerFactory
    {
        public static IServiceInstaller GetDockerBuildInstaller(this ServiceSource source)
        {
            if (source.Type != SourceType.DockerBuild) throw new InvalidOperationException("Invalid SourceType");
            if (string.IsNullOrWhiteSpace(source.BuildContext))
                throw new InvalidOperationException("BuildContext must have a value");

            return new DockerBuildInstaller(source.BuildContext, source.Tag);
        }

        public static IServiceInstaller GetDockerImageInstaller(this ServiceSource source)
        {
            if (source.Type != SourceType.DockerImage) throw new InvalidOperationException("Invalid SourceType");
            if (string.IsNullOrWhiteSpace(source.ImageName))
                throw new InvalidOperationException("ImageName must have a value");

            return new DockerImageInstaller(source.ImageName, source.Tag);
        }

        public static IServiceInstaller GetDotnetToolInstaller(this ServiceSource source)
        {
            if (source.Type != SourceType.DotnetTool) throw new InvalidOperationException("Invalid SourceType");
            if (string.IsNullOrWhiteSpace(source.ToolName))
                throw new InvalidOperationException("ToolName must have a value");

            return new DotnetToolInstaller(source.ToolName, source.ExtraArgs);
        }

        public static IServiceInstaller GetGitInstaller(this ServiceSource source)
        {
            if (source.Type != SourceType.Git) throw new InvalidOperationException("Invalid SourceType");
            if (string.IsNullOrWhiteSpace(source.CloneUrl))
                throw new InvalidOperationException("GitCloneUrl must have a value");

            return new GitInstaller(source.CloneUrl);
        }

        public static IServiceInstaller GetLocalDirectoryInstaller(this ServiceSource source)
        {
            if (source.Type != SourceType.LocalDirectory) throw new InvalidOperationException("Invalid SourceType");

            return NoOpInstaller.Value;
        }
    }
}
