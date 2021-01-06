using FluentValidation;

namespace Cli.Services.Sources.Validation
{
    internal class DockerBuildValidator : AbstractValidator<ServiceSource>
    {
        public DockerBuildValidator()
        {
            RuleFor(x => x.Type).Equal(SourceType.DockerBuild);
            RuleFor(x => x.BuildContext).NotNull().NotEmpty();
            RuleFor(x => x.Tag).NotEmpty().WithSeverity(Severity.Info);
        }
    }
}
