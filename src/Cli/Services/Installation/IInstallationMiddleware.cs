using Cli.Internal;

namespace Cli.Services.Installation
{
    internal interface IInstallationMiddleware : IPipelineBehaviour<InstallationContext>
    {
    }
}
