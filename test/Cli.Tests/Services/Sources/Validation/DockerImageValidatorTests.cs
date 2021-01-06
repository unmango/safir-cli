using System.Collections.Generic;
using Cli.Services;
using Cli.Services.Sources.Validation;
using FluentValidation.TestHelper;
using Xunit;

namespace Cli.Tests.Services.Sources.Validation
{
    public class DockerImageValidatorTests
    {
        private readonly DockerImageValidator _validator = new();
        
        [Theory]
        [MemberData(nameof(SourceTypeValuesExcept), SourceType.DockerImage)]
        public void RequiresDockerImageSourceType(SourceType type)
        {
            var source = new ServiceSource { Type = type };

            var result = _validator.TestValidate(source);

            result.ShouldHaveValidationErrorFor(x => x.Type);
        }

        [Theory]
        [ClassData(typeof(NullOrWhitespaceStrings))]
        public void RequiresImageName(string? imageName)
        {
            var source = new ServiceSource {
                Type = SourceType.DockerImage,
                ImageName = imageName,
            };

            var result = _validator.TestValidate(source);

            result.ShouldHaveValidationErrorFor(x => x.ImageName);
        }

        [Fact]
        public void ValidatesForDockerImage()
        {
            var source = new ServiceSource {
                Type = SourceType.DockerImage,
                ImageName = "image",
            };

            var result = _validator.TestValidate(source);

            result.ShouldNotHaveAnyValidationErrors();
        }

        private static IEnumerable<object[]> SourceTypeValuesExcept(SourceType type) => SourceTypeValues.Except(type);
    }
}
