using System;
using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;

namespace Cli.Services.Sources.Validation
{
    internal static class ServiceSourceExtensions
    {
        private static readonly IValidator<ServiceSource> _dockerBuild = new DockerBuildValidator();
        private static readonly IValidator<ServiceSource> _dockerImage = new DockerImageValidator();
        private static readonly IValidator<ServiceSource> _dotnetTool = new DotnetToolValidator();
        private static readonly IValidator<ServiceSource> _git = new GitValidator();
        private static readonly IValidator<ServiceSource> _localDirectory = new LocalDirectoryValidator();

        public static ValidationResult ValidateDockerBuild(
            this ServiceSource source,
            Action<ValidationStrategy<ServiceSource>>? options = null)
            => _dockerBuild.Validate(source, options ?? (_ => { }));

        public static ValidationResult ValidateDockerImage(
            this ServiceSource source,
            Action<ValidationStrategy<ServiceSource>>? options = null)
            => _dockerImage.Validate(source, options ?? (_ => { }));

        public static ValidationResult ValidateDotnetTool(
            this ServiceSource source,
            Action<ValidationStrategy<ServiceSource>>? options = null)
            => _dotnetTool.Validate(source, options ?? (_ => { }));

        public static ValidationResult ValidateGit(
            this ServiceSource source,
            Action<ValidationStrategy<ServiceSource>>? options = null)
            => _git.Validate(source, options ?? (_ => { }));

        public static ValidationResult ValidateLocalDirectory(
            this ServiceSource source,
            Action<ValidationStrategy<ServiceSource>>? options = null)
            => _localDirectory.Validate(source, options ?? (_ => { }));
    }
}
