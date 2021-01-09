using Cli.Internal.Pipeline;

namespace Cli.Services.Installation
{
    internal interface IInstallationMiddleware : IPipelineBehaviour<InstallationContext>
    {
    }
}
