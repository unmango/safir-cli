using System.Threading;
using System.Threading.Tasks;
// ReSharper disable NotAccessedField.Local

namespace Cli.Services.Installers
{
    internal class GitInstaller : IServiceInstaller
    {
        private readonly string _cloneUrl;

        public GitInstaller(string cloneUrl)
        {
            _cloneUrl = cloneUrl;
        }
        
        public ValueTask InstallAsync(InstallationContext context, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}
