using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cli.Services.Installation.Installers
{
    internal sealed class SourceInstallerMiddleware<T> : SourceMiddleware<T>
        where T : IServiceSource
    {
        public SourceInstallerMiddleware(ISourceInstaller<T> installer) : base(installer)
        {
        }

        public override async ValueTask InvokeAsync(
            InstallationContext context,
            Func<InstallationContext, ValueTask> next,
            CancellationToken cancellationToken = default)
        {
            await Installer.InstallAsync(context, cancellationToken);
            await next(context);
        }
    }
}
