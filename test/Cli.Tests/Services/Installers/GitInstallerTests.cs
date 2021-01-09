using System;
using System.Threading.Tasks;
using Cli.Services;
using Cli.Services.Installers;
using Cli.Services.Installers.Vcs;
using LibGit2Sharp;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace Cli.Tests.Services.Installers
{
    public class GitInstallerTests
    {
        private const string CloneUrl = "url";
        private readonly AutoMocker _mocker = new();
        private readonly GitInstaller _installer;

        public GitInstallerTests()
        {
            _installer = _mocker.Get<GitInstaller>();
        }

        [Theory]
        [InlineData("not a url")]
        [InlineData("www.example.com")]
        [InlineData("svn://192.168.420.69/svn-repo")]
        public void Constructor_RequiresValidGitUrl(string url)
        {
            var repository = _mocker.GetMock<IRepositoryFunctions>();
            
            Assert.Throws<ArgumentException>(() => new GitInstaller(url, repository.Object));
        }

        [Fact]
        public async Task InstallAsync_ClonesRepository()
        {
            const string workingDir = "workdir";
            var context = new InstallationContext(
                workingDir,
                new ServiceEntry(),
                new[] { new ServiceSource() });
            var repository = _mocker.GetMock<IRepositoryFunctions>();

            await _installer.InstallAsync(context);
            
            repository.Verify(x => x.Clone(CloneUrl, workingDir, It.IsAny<CloneOptions>()));
        }

        [Fact]
        public async Task InstallAsync_SkipsCloneWhenRepositoryExists()
        {
            const string workingDir = "workdir";
            var context = new InstallationContext(
                workingDir,
                new ServiceEntry(),
                new[] { new ServiceSource() });
            var repository = _mocker.GetMock<IRepositoryFunctions>();
            repository.Setup(x => x.IsValid(workingDir)).Returns(true);

            await _installer.InstallAsync(context);
            
            repository.Verify(x => x.Clone(CloneUrl, workingDir, It.IsAny<CloneOptions>()), Times.Never);
        }
    }
}
