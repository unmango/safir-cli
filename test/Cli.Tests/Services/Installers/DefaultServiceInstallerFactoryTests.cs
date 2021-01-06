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
        private readonly DefaultServiceInstallerFactory _factory;
        
        public DefaultServiceInstallerFactoryTests()
        {
            _factory = new DefaultServiceInstallerFactory();
        }
        
        [Theory]
        [MemberData(nameof(SourceTypeValuesExcept), SourceType.DockerBuild)]
        public void GetDockerBuildInstaller_RequiresDockerBuildSourceType(SourceType type)
        {
            var source = new ServiceSource { Type = type };

            Assert.Throws<InvalidOperationException>(() => _factory.GetDockerBuildInstaller(source));
        }

        [Theory]
        [ClassData(typeof(NullOrWhitespaceStrings))]
        public void GetDockerBuildInstaller_RequiresBuildContext(string? buildContext)
        {
            var source = new ServiceSource {
                Type = SourceType.DockerBuild,
                BuildContext = buildContext,
            };

            Assert.Throws<InvalidOperationException>(() => _factory.GetDockerBuildInstaller(source));
        }

        [Fact]
        public void GetDockerBuildInstaller_GetsDockerBuildInstaller()
        {
            var source = new ServiceSource {
                Type = SourceType.DockerBuild,
                BuildContext = "context",
            };

            var result = _factory.GetDockerBuildInstaller(source);

            Assert.IsType<DockerBuildInstaller>(result);
        }
        
        [Theory]
        [MemberData(nameof(SourceTypeValuesExcept), SourceType.DockerImage)]
        public void GetDockerImageInstaller_RequiresDockerBuildSourceType(SourceType type)
        {
            var source = new ServiceSource { Type = type };

            Assert.Throws<InvalidOperationException>(() => _factory.GetDockerImageInstaller(source));
        }

        [Theory]
        [ClassData(typeof(NullOrWhitespaceStrings))]
        public void GetDockerImageInstaller_RequiresImageName(string? imageName)
        {
            var source = new ServiceSource {
                Type = SourceType.DockerImage,
                ImageName = imageName,
            };

            Assert.Throws<InvalidOperationException>(() => _factory.GetDockerImageInstaller(source));
        }

        [Fact]
        public void GetDockerImageInstaller_GetsDockerImageInstaller()
        {
            var source = new ServiceSource {
                Type = SourceType.DockerImage,
                ImageName = "image",
            };

            var result = _factory.GetDockerImageInstaller(source);

            Assert.IsType<DockerImageInstaller>(result);
        }

        [Theory]
        [MemberData(nameof(SourceTypeValuesExcept), SourceType.Git)]
        public void GetGitInstaller_RequiresGitSourceType(SourceType type)
        {
            var source = new ServiceSource { Type = type };

            Assert.Throws<InvalidOperationException>(() => _factory.GetGitInstaller(source));
        }

        [Theory]
        [ClassData(typeof(NullOrWhitespaceStrings))]
        public void GetGitInstaller_RequiresCloneUrl(string? cloneUrl)
        {
            var source = new ServiceSource {
                Type = SourceType.Git,
                CloneUrl = cloneUrl,
            };

            Assert.Throws<InvalidOperationException>(() => _factory.GetGitInstaller(source));
        }

        [Fact]
        public void GetGitInstaller_GetsGitInstaller()
        {
            var source = new ServiceSource {
                Type = SourceType.Git,
                CloneUrl = "a url",
            };

            var result = _factory.GetGitInstaller(source);

            Assert.IsType<GitInstaller>(result);
        }

        [Theory]
        [MemberData(nameof(SourceTypeValuesExcept), SourceType.DotnetTool)]
        public void GetDotnetToolInstaller_RequiresDotnetToolSourceType(SourceType type)
        {
            var source = new ServiceSource { Type = type };

            Assert.Throws<InvalidOperationException>(() => _factory.GetDotnetToolInstaller(source));
        }

        [Theory]
        [ClassData(typeof(NullOrWhitespaceStrings))]
        public void GetDotnetToolInstaller_RequiresToolName(string? toolName)
        {
            var source = new ServiceSource {
                Type = SourceType.DotnetTool,
                ToolName = toolName,
            };

            Assert.Throws<InvalidOperationException>(() => _factory.GetDotnetToolInstaller(source));
        }

        [Fact]
        public void GetDotnetToolInstaller_GetsDotnetToolInstaller()
        {
            var source = new ServiceSource {
                Type = SourceType.DotnetTool,
                ToolName = "name",
            };

            var result = _factory.GetDotnetToolInstaller(source);

            Assert.IsType<DotnetToolInstaller>(result);
        }

        [Theory]
        [MemberData(nameof(SourceTypeValuesExcept), SourceType.LocalDirectory)]
        public void GetLocalDirectoryInstaller_RequiresLocalDirectorySourceType(SourceType type)
        {
            var source = new ServiceSource { Type = type };

            Assert.Throws<InvalidOperationException>(() => _factory.GetLocalDirectoryInstaller(source));
        }

        [Fact]
        public void GetLocalDirectoryInstaller_GetsNoOpInstaller()
        {
            var source = new ServiceSource { Type = SourceType.LocalDirectory };

            var result = _factory.GetLocalDirectoryInstaller(source);

            Assert.IsType<NoOpInstaller>(result);
        }

        private static IEnumerable<object[]> SourceTypeValuesExcept(SourceType type) => SourceTypeValues.Except(type);
    }
}
