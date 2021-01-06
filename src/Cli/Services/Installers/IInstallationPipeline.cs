using System.Threading;
using System.Threading.Tasks;

namespace Cli.Services.Installers
{
    internal interface IInstallationPipeline
    {
        ValueTask InstallAsync(ServiceEntry service, CancellationToken cancellationToken = default);
    }
}
