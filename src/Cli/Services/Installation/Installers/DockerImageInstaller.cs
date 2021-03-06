using System.Threading;
using System.Threading.Tasks;

// ReSharper disable NotAccessedField.Local

namespace Cli.Services.Installation.Installers
{
    internal class DockerImageInstaller : IServiceInstaller
    {
        private readonly string _imageName;
        private readonly string? _tag;

        public DockerImageInstaller(string imageName, string? tag)
        {
            _imageName = imageName;
            _tag = tag;
        }
        
        public ValueTask InstallAsync(InstallationContext context, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}
