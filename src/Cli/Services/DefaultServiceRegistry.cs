using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cli.Services
{
    internal class DefaultServiceRegistry : IServiceRegistry
    {
        private readonly IServiceFactory _factory;
        private readonly List<IServiceSource> _sources;

        public DefaultServiceRegistry(IServiceFactory factory, IEnumerable<IServiceSource> sources)
        {
            _factory = factory;
            _sources = sources.ToList();
        }

        public IReadOnlyList<ServiceEntry> Services { get; } = new List<ServiceEntry>();

        public async Task<IService> GetServiceAsync(ServiceEntry service, CancellationToken cancellationToken = default)
        {
            if (service == null) throw new ArgumentNullException(nameof(service));

            var source = _sources.Find(x => x.Satisfies(service));

            if (source == null)
                throw new NotSupportedException($"No service source found to support service {service.Name}");

            var canInitialize = await source.CanInitializeAsync(cancellationToken);

            if (canInitialize.Failed)
                throw new NotSupportedException(
                    $"Can't initialize source {source.GetType()}: {canInitialize.FailureMessage}");

            var workingDirectory = string.Empty; // TODO

            if (!await source.IsInitializedAsync(service, workingDirectory, cancellationToken))
                await source.InitializeAsync(service, workingDirectory, cancellationToken);

            return _factory.Create(service);
        }
    }
}
