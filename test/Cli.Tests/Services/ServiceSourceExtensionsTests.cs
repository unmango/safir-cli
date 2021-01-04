using System;
using Cli.Services;
using Cli.Services.Installers;
using Xunit;

namespace Cli.Tests.Services
{
    public class ServiceSourceExtensionsTests
    {
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
        [InlineData(SourceType.DotnetTool)]
        [InlineData(SourceType.LocalDirectory)]
        [InlineData((SourceType)69)]
        public void GetGitInstaller_RequiresGitSourceType(SourceType type)
        {
            var source = new ServiceSource { Type = type };

            Assert.Throws<InvalidOperationException>(() => source.GetGitInstaller());
        }

        [Theory]
        [InlineData((string?)null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\t")]
        [InlineData("\n")]
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
        [InlineData(SourceType.Git)]
        [InlineData(SourceType.LocalDirectory)]
        [InlineData((SourceType)69)]
        public void GetDotnetToolInstaller_RequiresGitSourceType(SourceType type)
        {
            var source = new ServiceSource { Type = type };

            Assert.Throws<InvalidOperationException>(() => source.GetDotnetToolInstaller());
        }

        [Theory]
        [InlineData((string?)null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\t")]
        [InlineData("\n")]
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

        [Fact]
        public void GetLocalDirectoryInstaller_GetsNoOpInstaller()
        {
            var source = new ServiceSource { Type = SourceType.LocalDirectory };

            var result = source.GetLocalDirectoryInstaller();

            Assert.IsType<NoOpInstaller>(result);
        }

        [Fact]
        public void OrderByPriority_OrdersByPriority()
        {
            var sources = new[] {
                new ServiceSource { Priority = 1 },
                new ServiceSource { Priority = 0 },
            };

            var ordered = sources.OrderByPriority();
            
            Assert.Collection(
                ordered,
                x => Assert.Equal(0, x.Priority),
                x => Assert.Equal(1, x.Priority));
        }

        [Fact]
        public void OrderByPriority_OrdersByPriorityWhenPriorityNotSet()
        {
            var sources = new[] {
                new ServiceSource { Priority = null },
                new ServiceSource { Priority = 0 },
            };

            var ordered = sources.OrderByPriority();
            
            Assert.Collection(
                ordered,
                x => Assert.Equal(0, x.Priority),
                x => Assert.False(x.Priority.HasValue));
        }

        [Fact]
        public void HighestPriority_ReturnsHighestPriority()
        {
            var sources = new[] {
                new ServiceSource { Priority = 1 },
                new ServiceSource { Priority = 0 },
            };

            var hightest = sources.HighestPriority();
            
            Assert.Equal(0, hightest.Priority);
        }

        [Fact]
        public void HighestPriority_ReturnsHighestPriorityMatchingPredicate()
        {
            const string expected = "expected";
            var sources = new[] {
                new ServiceSource { Priority = 1, CloneUrl = expected },
                new ServiceSource { Priority = 0 },
            };

            var hightest = sources.HighestPriority(x => x.CloneUrl == expected);
            
            Assert.Equal(1, hightest.Priority);
        }

        // TODO: Review is this is functionality I want...
        [Fact]
        public void HighestPriority_ThrowsWhenNoSources()
        {
            var sources = Array.Empty<ServiceSource>();

            Assert.Throws<InvalidOperationException>(() => sources.HighestPriority());
        }

        // TODO: Review is this is functionality I want...
        [Fact]
        public void HighestPriority_ThrowsWhenNoSourcesMatchPredicate()
        {
            var sources = new[] { new ServiceSource { Priority = 0 } };

            Assert.Throws<InvalidOperationException>(
                () => sources.HighestPriority(x => !string.IsNullOrWhiteSpace(x.CloneUrl)));
        }

        [Fact]
        public void HighestPriorityOrDefault_ReturnsHighestPriority()
        {
            var sources = new[] {
                new ServiceSource { Priority = 1 },
                new ServiceSource { Priority = 0 },
            };

            var hightest = sources.HighestPriorityOrDefault();
            
            Assert.NotNull(hightest);
            Assert.Equal(0, hightest!.Priority);
        }

        [Fact]
        public void HighestPriorityOrDefault_ReturnsHighestPriorityMatchingPredicate()
        {
            const string expected = "expected";
            var sources = new[] {
                new ServiceSource { Priority = 1, CloneUrl = expected },
                new ServiceSource { Priority = 0 },
            };

            var hightest = sources.HighestPriorityOrDefault(x => x.CloneUrl == expected);
            
            Assert.NotNull(hightest);
            Assert.Equal(1, hightest!.Priority);
        }

        [Fact]
        public void HighestPriority_ReturnsNullWhenNoSources()
        {
            var sources = Array.Empty<ServiceSource>();

            var highest = sources.HighestPriorityOrDefault();
            
            Assert.Null(highest);
        }

        [Fact]
        public void HighestPriority_ReturnsNullWhenNoSourcesMatchPredicate()
        {
            var sources = new[] { new ServiceSource { Priority = 0 } };

            var highest = sources.HighestPriorityOrDefault(x => !string.IsNullOrWhiteSpace(x.CloneUrl));
            
            Assert.Null(highest);
        }
    }
}
