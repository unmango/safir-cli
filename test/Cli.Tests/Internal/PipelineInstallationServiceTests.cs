using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cli.Internal;
using Cli.Services;
using Cli.Services.Installers;
using Cli.Services.Sources;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace Cli.Tests.Internal
{
    public class PipelineInstallationServiceTests
    {
        private const string WorkingDirectory = "workingDirectory";
        private static readonly AutoMocker _mocker = new();
        private readonly ServiceEntry _defaultService = new();
        private readonly PipelineInstallationService _service = _mocker.Get<PipelineInstallationService>();

        public PipelineInstallationServiceTests()
        {
            _mocker.GetMock<IServiceDirectory>()
                .Setup(x => x.GetInstallationDirectory(It.IsAny<IEnumerable<string>?>()))
                .Returns(WorkingDirectory);
        }

        // TODO: Probs break this out into two tests
        [Theory]
        [InlineData((string?)null)]
        [InlineData("directory")]
        public async Task PassesDirectory(string? directory)
        {
            if (directory == null)
                await _service.InstallAsync(_defaultService);
            else
                await _service.InstallAsync(_defaultService, directory);

            _mocker.GetMock<IServiceDirectory>().Verify(x => x.GetInstallationDirectory(It.Is<IEnumerable<string>>(
                directories => directory == null
                    ? !directories.Any()
                    : directories.Contains(directory))));
        }

        [Fact]
        public async Task InvokesWithCorrectContext()
        {
            var expectedSources = new[] { new ServiceSource() };
            var serviceEntry = _defaultService with {
                Sources = expectedSources
            };
            var installer = new Mock<IPipelineServiceInstaller>();
            _mocker.Use<IEnumerable<IPipelineServiceInstaller>>(new[] { installer.Object });
            
            await _service.InstallAsync(serviceEntry);
            
            installer.Verify(x => x.InvokeAsync(
                It.Is<InstallationContext>(context =>
                    context.WorkingDirectory == WorkingDirectory &&
                    context.Service == serviceEntry &&
                    context.Sources == expectedSources),
                It.IsAny<Func<InstallationContext, ValueTask>>(),
                It.IsAny<CancellationToken>()));
        }
    }
}
