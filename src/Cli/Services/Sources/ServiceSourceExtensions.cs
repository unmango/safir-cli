using System;
using System.Diagnostics.CodeAnalysis;
using Cli.Services.Sources.Validation;
using FluentValidation;

namespace Cli.Services.Sources
{
    internal static class ServiceSourceExtensions
    {
        private static readonly IValidator<ServiceSource> _dockerBuild = new DockerBuildValidator();
        private static readonly IValidator<ServiceSource> _dockerImage = new DockerImageValidator();
        private static readonly IValidator<ServiceSource> _dotnetTool = new DotnetToolValidator();
        private static readonly IValidator<ServiceSource> _git = new GitValidator();
        private static readonly IValidator<ServiceSource> _localDirectory = new LocalDirectoryValidator();

        public static DockerBuildSource GetDockerBuildSource(this ServiceSource source)
        {
            _dockerBuild.ValidateAndThrow(source);
            return new DockerBuildSource(source.BuildContext!, source.Tag);
        }

        public static DockerImageSource GetDockerImageSource(this ServiceSource source)
        {
            _dockerImage.ValidateAndThrow(source);
            return new DockerImageSource(source.ImageName!, source.Tag);
        }

        public static DotnetToolSource GetDotnetToolSource(this ServiceSource source)
        {
            _dotnetTool.ValidateAndThrow(source);
            return new DotnetToolSource(source.ToolName!, source.ExtraArgs);
        }

        public static GitSource GetGitSource(this ServiceSource source)
        {
            _git.ValidateAndThrow(source);
            return new GitSource(source.CloneUrl!);
        }

        public static LocalDirectorySource GetLocalDirectorySource(this ServiceSource source)
        {
            _localDirectory.ValidateAndThrow(source);
            return new LocalDirectorySource(source.SourceDirectory!);
        }

        public static bool TryGetDockerBuildSource(
            this ServiceSource source,
            [MaybeNullWhen(false)] out DockerBuildSource dockerBuild)
            => source.TryGet(
                _dockerBuild,
                x => new DockerBuildSource(x.BuildContext!, x.Tag),
                out dockerBuild);

        public static bool TryGetDockerImageSource(
            this ServiceSource source,
            [MaybeNullWhen(false)] out DockerImageSource dockerImage)
            => source.TryGet(
                _dockerImage,
                x => new DockerImageSource(x.ImageName!, x.Tag),
                out dockerImage);

        public static bool TryGetDotnetToolSource(
            this ServiceSource source,
            [MaybeNullWhen(false)] out DotnetToolSource dotnetTool)
            => source.TryGet(
                _dotnetTool,
                x => new DotnetToolSource(x.ToolName!, x.ExtraArgs),
                out dotnetTool);

        public static bool TryGetGitSource(
            this ServiceSource source,
            [MaybeNullWhen(false)] out GitSource git)
            => source.TryGet(
                _git,
                x => new GitSource(x.CloneUrl!),
                out git);

        public static bool TryGetLocalDirectorySource(
            this ServiceSource source,
            [MaybeNullWhen(false)] out LocalDirectorySource localDirectory)
            => source.TryGet(
                _localDirectory,
                x => new LocalDirectorySource(x.SourceDirectory!),
                out localDirectory);

        private static bool TryGet<T>(
            this ServiceSource source,
            IValidator<ServiceSource> validator,
            Func<ServiceSource, T> factory,
            [MaybeNullWhen(false)] out T gotten)
        {
            gotten = default;
            var result = validator.Validate(source);
            if (result.IsValid) gotten = factory(source);
            return result.IsValid;
        }
    }
}
