using System;
using System.Collections.Generic;
using System.Linq;
using Cli.Services;
using Cli.Services.Installers;
using Xunit;

namespace Cli.Tests.Services.Installers
{
    public class DefaultServiceInstallerFactoryTests
    {
        [Theory]
        [InlineData(SourceType.Docker)]
        [InlineData(SourceType.DockerImage)]
        public void GetInstaller_GetsDockerImageInstaller(SourceType type)
        {
            var source = new ServiceSource {
                Type = type,
                ImageName = "image",
            };

            var result = source.GetInstaller();

            Assert.IsType<DockerImageInstaller>(result);
        }
        
        [Theory]
        [InlineData(SourceType.Docker)]
        [InlineData(SourceType.DockerBuild)]
        public void GetInstaller_GetsDockerBuildInstaller(SourceType type)
        {
            var source = new ServiceSource {
                Type = type,
                BuildContext = "context",
            };

            var result = source.GetInstaller();

            Assert.IsType<DockerBuildInstaller>(result);
        }
        
        [Fact]
        public void GetInstaller_GetsGitInstaller()
        {
            var source = new ServiceSource {
                Type = SourceType.Git,
                CloneUrl = "a url",
            };

            var result = source.GetInstaller();

            Assert.IsType<GitInstaller>(result);
        }

        [Fact]
        public void GetInstaller_GetsDotnetToolInstaller()
        {
            var source = new ServiceSource {
                Type = SourceType.DotnetTool,
                ToolName = "tool",
            };

            var result = source.GetInstaller();

            Assert.IsType<DotnetToolInstaller>(result);
        }

        [Fact]
        public void GetInstaller_GetsLocalDirectoryInstaller()
        {
            var source = new ServiceSource { Type = SourceType.LocalDirectory };

            var result = source.GetInstaller();

            Assert.IsType<NoOpInstaller>(result);
        }

        [Fact]
        public void GetInstaller_ThrowsWhenTypeIsNotSet()
        {
            var source = new ServiceSource { Type = null };

            Assert.Throws<InvalidOperationException>(() => source.GetInstaller());
        }

        [Fact]
        public void GetInstaller_ThrowsWhenTypeIsUnrecognized()
        {
            var source = new ServiceSource { Type = (SourceType)69 };

            Assert.Throws<NotSupportedException>(() => source.GetInstaller());
        }

        [Theory]
        [MemberData(nameof(SourceTypeValuesExcept), SourceType.Docker)]
        public void GetDockerInstaller_RequiresDockerSourceType(SourceType type)
        {
            var source = new ServiceSource { Type = type };

            Assert.Throws<InvalidOperationException>(() => source.GetDockerInstaller());
        }

        [Fact]
        public void GetDockerInstaller_InfersDockerBuildInstaller()
        {
            var source = new ServiceSource {
                Type = SourceType.Docker,
                BuildContext = "context",
            };

            var result = source.GetDockerInstaller();

            Assert.IsType<DockerBuildInstaller>(result);
        }

        [Fact]
        public void GetDockerInstaller_InfersDockerImageInstaller()
        {
            var source = new ServiceSource {
                Type = SourceType.Docker,
                ImageName = "image",
            };

            var result = source.GetDockerInstaller();

            Assert.IsType<DockerImageInstaller>(result);
        }
        
        [Theory]
        [MemberData(nameof(SourceTypeValuesExcept), SourceType.DockerBuild)]
        public void GetDockerBuildInstaller_RequiresDockerBuildSourceType(SourceType type)
        {
            var source = new ServiceSource { Type = type };

            Assert.Throws<InvalidOperationException>(() => source.GetDockerBuildInstaller());
        }

        [Theory]
        [MemberData(nameof(NullOrWhitespaceStrings))]
        public void GetDockerBuildInstaller_RequiresBuildContext(string? buildContext)
        {
            var source = new ServiceSource {
                Type = SourceType.DockerBuild,
                BuildContext = buildContext,
            };

            Assert.Throws<InvalidOperationException>(() => source.GetDockerBuildInstaller());
        }

        [Fact]
        public void GetDockerBuildInstaller_GetsDockerBuildInstaller()
        {
            var source = new ServiceSource {
                Type = SourceType.DockerBuild,
                BuildContext = "context",
            };

            var result = source.GetDockerBuildInstaller();

            Assert.IsType<DockerBuildInstaller>(result);
        }
        
        [Theory]
        [MemberData(nameof(SourceTypeValuesExcept), SourceType.DockerImage)]
        public void GetDockerImageInstaller_RequiresDockerBuildSourceType(SourceType type)
        {
            var source = new ServiceSource { Type = type };

            Assert.Throws<InvalidOperationException>(() => source.GetDockerImageInstaller());
        }

        [Theory]
        [MemberData(nameof(NullOrWhitespaceStrings))]
        public void GetDockerImageInstaller_RequiresImageName(string? imageName)
        {
            var source = new ServiceSource {
                Type = SourceType.DockerImage,
                ImageName = imageName,
            };

            Assert.Throws<InvalidOperationException>(() => source.GetDockerImageInstaller());
        }

        [Fact]
        public void GetDockerImageInstaller_GetsDockerImageInstaller()
        {
            var source = new ServiceSource {
                Type = SourceType.DockerImage,
                ImageName = "image",
            };

            var result = source.GetDockerImageInstaller();

            Assert.IsType<DockerImageInstaller>(result);
        }

        [Theory]
        [MemberData(nameof(SourceTypeValuesExcept), SourceType.Git)]
        public void GetGitInstaller_RequiresGitSourceType(SourceType type)
        {
            var source = new ServiceSource { Type = type };

            Assert.Throws<InvalidOperationException>(() => source.GetGitInstaller());
        }

        [Theory]
        [MemberData(nameof(NullOrWhitespaceStrings))]
        public void GetGitInstaller_RequiresCloneUrl(string? cloneUrl)
        {
            var source = new ServiceSource {
                Type = SourceType.Git,
                CloneUrl = cloneUrl,
            };

            Assert.Throws<InvalidOperationException>(() => source.GetGitInstaller());
        }

        [Fact]
        public void GetGitInstaller_GetsGitInstaller()
        {
            var source = new ServiceSource {
                Type = SourceType.Git,
                CloneUrl = "a url",
            };

            var result = source.GetGitInstaller();

            Assert.IsType<GitInstaller>(result);
        }

        [Theory]
        [MemberData(nameof(SourceTypeValuesExcept), SourceType.DotnetTool)]
        public void GetDotnetToolInstaller_RequiresDotnetToolSourceType(SourceType type)
        {
            var source = new ServiceSource { Type = type };

            Assert.Throws<InvalidOperationException>(() => source.GetDotnetToolInstaller());
        }

        [Theory]
        [MemberData(nameof(NullOrWhitespaceStrings))]
        public void GetDotnetToolInstaller_RequiresToolName(string? toolName)
        {
            var source = new ServiceSource {
                Type = SourceType.DotnetTool,
                ToolName = toolName,
            };

            Assert.Throws<InvalidOperationException>(() => source.GetDotnetToolInstaller());
        }

        [Fact]
        public void GetDotnetToolInstaller_GetsDotnetToolInstaller()
        {
            var source = new ServiceSource {
                Type = SourceType.DotnetTool,
                ToolName = "name",
            };

            var result = source.GetDotnetToolInstaller();

            Assert.IsType<DotnetToolInstaller>(result);
        }

        [Theory]
        [MemberData(nameof(SourceTypeValuesExcept), SourceType.LocalDirectory)]
        public void GetLocalDirectoryInstaller_RequiresLocalDirectorySourceType(SourceType type)
        {
            var source = new ServiceSource { Type = type };

            Assert.Throws<InvalidOperationException>(() => source.GetLocalDirectoryInstaller());
        }

        [Fact]
        public void GetLocalDirectoryInstaller_GetsNoOpInstaller()
        {
            var source = new ServiceSource { Type = SourceType.LocalDirectory };

            var result = source.GetLocalDirectoryInstaller();

            Assert.IsType<NoOpInstaller>(result);
        }

        private static IEnumerable<object[]> SourceTypeValuesExcept(SourceType type)
        {
            return Enum.GetValues<SourceType>()
                .Except(new[] { type })
                .Concat(new[] { (SourceType)69 })
                .Select(x => new object[] { x });
        }

        private static IEnumerable<object[]> NullOrWhitespaceStrings()
        {
            return new[] {
                null,
                "",
                " ",
                "\t",
                "\n",
            }.Select(x => new object[] { x });
        }
    }
}
