using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cli.Services.Sources;

// ReSharper disable NotAccessedField.Local

namespace Cli.Services.Installers
{
    internal class GitInstaller : IServiceInstaller, IPipelineServiceInstaller
    {
        private readonly string _cloneUrl;

        public GitInstaller(string cloneUrl)
        {
            _cloneUrl = cloneUrl;
        }

        public ValueTask InstallAsync(InstallationContext context, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public bool AppliesTo(InstallationContext context)
        {
            return context.Sources.Any(x => x.TryGetGitSource(out _));
        }

        public ValueTask InvokeAsync(
            InstallationContext context,
            Func<InstallationContext, ValueTask> next,
            CancellationToken cancellationToken = default)
            => AppliesTo(context)
                ? InstallAsync(context, cancellationToken)
                : next(context);
    }
}
