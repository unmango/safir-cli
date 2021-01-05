using System;
using System.Threading.Tasks;
using Cli.Services;
using Cli.Services.Installers;
using Cli.Services.Installers.Vcs;
using LibGit2Sharp;
using Moq;
using Xunit;

namespace Cli.Tests.Services.Installers
{
    public class GitInstallerTests
    {
        private const string CloneUrl = "url";
        private readonly Mock<IRepositoryFunctions> _repository = new();
        private readonly Mock<IRemoteFunctions> _remote = new();
        private readonly GitInstaller _installer;

        public GitInstallerTests()
        {
            _remote.Setup(x => x.IsValidName(It.IsAny<string>())).Returns(true);
            _installer = new GitInstaller(CloneUrl, _repository.Object, _remote.Object);
        }

        [Theory]
        [InlineData("not a url")]
        [InlineData("www.example.com")]
        [InlineData("svn://192.168.420.69/svn-repo")]
        public void Constructor_RequiresValidGitUrl(string url)
        {
            _remote.Setup(x => x.IsValidName(It.IsAny<string>())).Returns(false);
            
            Assert.Throws<ArgumentException>(() => new GitInstaller(url, _repository.Object, _remote.Object));
        }

        [Fact]
        public async Task InstallAsync_ClonesRepository()
        {
            const string workingDir = "workdir";
            var context = new InstallationContext(workingDir);

            await _installer.InstallAsync(context);
            
            _repository.Verify(x => x.Clone(CloneUrl, workingDir, It.IsAny<CloneOptions>()));
        }

        [Fact]
        public async Task InstallAsync_SkipsCloneWhenRepositoryExists()
        {
            const string workingDir = "workdir";
            var context = new InstallationContext(workingDir);
            _repository.Setup(x => x.IsValid(workingDir)).Returns(true);

            await _installer.InstallAsync(context);
            
            _repository.Verify(x => x.Clone(CloneUrl, workingDir, It.IsAny<CloneOptions>()), Times.Never);
        }
    }
}
