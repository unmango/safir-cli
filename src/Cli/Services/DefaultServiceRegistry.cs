using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cli.Services
{
    internal class DefaultServiceRegistry : IServiceRegistry
    {
        private readonly List<IServiceSource> _sources;

        public DefaultServiceRegistry(IEnumerable<IServiceSource> sources)
        {
            _sources = sources.ToList();
        }

        public IReadOnlyList<ServiceEntry> Services { get; } = new List<ServiceEntry>();

        public Task<IService> GetServiceAsync(ServiceEntry service)
        {
            if (service == null) throw new ArgumentNullException(nameof(service));

            var source = _sources.Find(x => x.Satisfies(service));

            if (source == null)
                throw new NotSupportedException($"No service source found to support service {service.Name}");

            throw new System.NotImplementedException();
        }
    }
}
