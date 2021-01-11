using System.Threading;
using System.Threading.Tasks;
using Cli.Services.Configuration;

namespace Cli.Services
{
    internal record ServiceEntryWrapper : IService
    {
        private readonly ServiceEntry _entry;

        public ServiceEntryWrapper(ServiceEntry entry)
        {
            _entry = entry;
        }
        
        public Task StartAsync(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}
