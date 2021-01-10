using System.Threading;
using System.Threading.Tasks;
using Cli.Services.Configuration;

namespace Cli.Services.Installation
{
    internal interface IInstallationService
    {
        Task InstallAsync(
            ServiceEntry service,
            string? directory = null,
            CancellationToken cancellationToken = default);
    }
}
