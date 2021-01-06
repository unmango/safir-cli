using System.Threading;
using System.Threading.Tasks;
using Cli.Services;

namespace Cli.Internal
{
    internal class PipelineInstallationService : IInstallationService
    {
        public Task InstallAsync(
            ServiceEntry service,
            string? directory = null,
            CancellationToken cancellationToken = default)
        {
            var workingDirectory = directory ?? string.Empty; // TODO

            var context = new InstallationContext(
                workingDirectory,
                service,
                // TODO: Select sources
                service.Sources);
            
            throw new System.NotImplementedException();
        }
    }
}
