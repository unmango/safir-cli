using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cli.Internal;

namespace Cli.Services.Installers
{
    internal class DefaultInstallationPipeline : IInstallationPipeline
    {
        // ReSharper disable once NotAccessedField.Local
        private readonly IEnumerable<IPipelineServiceInstaller> _installers;

        public DefaultInstallationPipeline(IEnumerable<IPipelineServiceInstaller> installers)
        {
            _installers = installers;
        }
        
        public ValueTask InstallAsync(InstallationContext context, CancellationToken cancellationToken = default)
        {
            return _installers.Where(x => x.AppliesTo(context))
                .BuildPipeline()
                .Invoke(context, _ => ValueTask.CompletedTask, cancellationToken);
        }
    }
}
