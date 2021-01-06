using System.Collections.Generic;
using Cli.Services;
using Cli.Services.Sources.Validation;
using FluentValidation.TestHelper;
using Xunit;

namespace Cli.Tests.Services.Sources.Validation
{
    public class DockerBuildValidatorTests
    {
        private readonly DockerBuildValidator _validator = new();
        
        [Theory]
        [MemberData(nameof(SourceTypeValuesExcept), SourceType.DockerBuild)]
        public void RequiresDockerBuildSourceType(SourceType type)
        {
            var source = new ServiceSource { Type = type };

            var result = _validator.TestValidate(source);

            result.ShouldHaveValidationErrorFor(x => x.Type);
        }

        [Theory]
        [ClassData(typeof(NullOrWhitespaceStrings))]
        public void RequiresBuildContext(string? buildContext)
        {
            var source = new ServiceSource {
                Type = SourceType.DockerBuild,
                BuildContext = buildContext,
            };

            var result = _validator.TestValidate(source);

            result.ShouldHaveValidationErrorFor(x => x.BuildContext);
        }

        [Fact]
        public void ValidatesForDockerBuild()
        {
            var source = new ServiceSource {
                Type = SourceType.DockerBuild,
                BuildContext = "context",
            };

            var result = _validator.TestValidate(source);

            result.ShouldNotHaveAnyValidationErrors();
        }

        private static IEnumerable<object[]> SourceTypeValuesExcept(SourceType type) => SourceTypeValues.Except(type);
    }
}