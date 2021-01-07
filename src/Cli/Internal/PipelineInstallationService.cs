using System.Threading;
using System.Threading.Tasks;
using Cli.Services;

namespace Cli.Internal
{
    internal class PipelineInstallationService : IInstallationService
    {
        private readonly IServiceDirectory _serviceDirectory;

        public PipelineInstallationService(IServiceDirectory serviceDirectory)
        {
            _serviceDirectory = serviceDirectory;
        }
        
        public Task InstallAsync(
            ServiceEntry service,
            string? directory = null,
            CancellationToken cancellationToken = default)
        {
            var workingDirectory = _serviceDirectory.GetInstallationDirectory(directory);

            var context = new InstallationContext(
                workingDirectory,
                service,
                // TODO: Select sources
                service.Sources);
            
            throw new System.NotImplementedException();
        }
    }
}
