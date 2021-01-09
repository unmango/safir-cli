using System.Threading;
using System.Threading.Tasks;

namespace Cli.Services.Installation.Installers
{
    internal interface IInstallationPipeline
    {
        ValueTask InstallAsync(InstallationContext context, CancellationToken cancellationToken = default);
    }
}
