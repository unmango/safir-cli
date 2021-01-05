using System;
using System.Collections.Generic;
using Cli.Services;
using Cli.Services.Installers;
using Moq;
using Xunit;

namespace Cli.Tests.Services
{
    public class ServiceInstallerFactoryExtensionsTests
    {
        private readonly Mock<IServiceInstallerFactory> _factory = new();
        
        [Theory]
        [InlineData(SourceType.Docker)]
        [InlineData(SourceType.DockerImage)]
        public void GetInstaller_GetsDockerImageInstaller(SourceType type)
        {
            var source = new ServiceSource {
                Type = type,
                ImageName = "image",
            };

            var result = _factory.Object.GetInstaller(source);

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

            var result = _factory.Object.GetInstaller(source);

            Assert.IsType<DockerBuildInstaller>(result);
        }
        
        [Fact]
        public void GetInstaller_GetsGitInstaller()
        {
            var source = new ServiceSource {
                Type = SourceType.Git,
                CloneUrl = "a url",
            };

            var result = _factory.Object.GetInstaller(source);

            Assert.IsType<GitInstaller>(result);
        }

        [Fact]
        public void GetInstaller_GetsDotnetToolInstaller()
        {
            var source = new ServiceSource {
                Type = SourceType.DotnetTool,
                ToolName = "tool",
            };

            var result = _factory.Object.GetInstaller(source);

            Assert.IsType<DotnetToolInstaller>(result);
        }

        [Fact]
        public void GetInstaller_GetsLocalDirectoryInstaller()
        {
            var source = new ServiceSource { Type = SourceType.LocalDirectory };

            var result = _factory.Object.GetInstaller(source);

            Assert.IsType<NoOpInstaller>(result);
        }

        [Fact]
        public void GetInstaller_ThrowsWhenTypeIsNotSet()
        {
            var source = new ServiceSource { Type = null };

            Assert.Throws<InvalidOperationException>(() => _factory.Object.GetInstaller(source));
        }

        [Fact]
        public void GetInstaller_ThrowsWhenTypeIsUnrecognized()
        {
            var source = new ServiceSource { Type = (SourceType)69 };

            Assert.Throws<NotSupportedException>(() => _factory.Object.GetInstaller(source));
        }
        
        [Theory]
        [MemberData(nameof(ValuesExcept), SourceType.Docker)]
        public void GetDockerInstaller_RequiresDockerSourceType(SourceType type)
        {
            var source = new ServiceSource { Type = type };

            Assert.Throws<InvalidOperationException>(() => _factory.Object.GetDockerInstaller(source));
        }

        [Fact]
        public void GetDockerInstaller_InfersDockerBuildInstaller()
        {
            var source = new ServiceSource {
                Type = SourceType.Docker,
                BuildContext = "context",
            };

            var result = _factory.Object.GetDockerInstaller(source);

            Assert.IsType<DockerBuildInstaller>(result);
        }

        [Fact]
        public void GetDockerInstaller_InfersDockerImageInstaller()
        {
            var source = new ServiceSource {
                Type = SourceType.Docker,
                ImageName = "image",
            };

            var result = _factory.Object.GetDockerInstaller(source);

            Assert.IsType<DockerImageInstaller>(result);
        }

        private static IEnumerable<object[]> ValuesExcept(SourceType type) => new SourceTypeValuesExcept(type);
    }
}
