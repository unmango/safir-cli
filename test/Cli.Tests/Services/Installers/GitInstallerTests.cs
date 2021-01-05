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
        private readonly GitInstaller _installer;

        public GitInstallerTests()
        {
            _installer = new GitInstaller(CloneUrl, _repository.Object);
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
