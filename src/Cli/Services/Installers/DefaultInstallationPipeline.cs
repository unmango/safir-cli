using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Cli.Services.Installers
{
    internal class DefaultInstallationPipeline : IInstallationPipeline
    {
        // ReSharper disable once NotAccessedField.Local
        private readonly IEnumerable<IServiceInstaller> _installers;

        public DefaultInstallationPipeline(IEnumerable<IServiceInstaller> installers)
        {
            _installers = installers;
        }
        
        public ValueTask InstallAsync(ServiceEntry service, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}
