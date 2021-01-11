using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Cli.Services.Configuration;
using Cli.Services.Sources;
using Microsoft.Extensions.Options;

namespace Cli.Services
{
    internal class DefaultServiceRegistry : IServiceRegistry
    {
        private const int StaticallyConfiguredServices = 2;
        
        private readonly IOptionsMonitor<ServiceOptions> _optionsMonitor;
        private readonly Dictionary<string, IService> _services = new(StaticallyConfiguredServices);
        private readonly Dictionary<IService, IEnumerable<IServiceSource>> _sources = new();

        public DefaultServiceRegistry(IOptionsMonitor<ServiceOptions> optionsMonitor)
        {
            _optionsMonitor = optionsMonitor;
            
            PopulateServices();
        }

        public IEnumerable<IService> Services => _services.Values;

        public IEnumerable<IServiceSource> GetSources(IService service)
        {
            return _sources[service];
        }

        public IServiceCommand GetCommand(IService service, CommandCategory category)
        {
            throw new System.NotImplementedException();
        }

        private void PopulateServices()
        {
            foreach (var (key, value) in _optionsMonitor.CurrentValue)
            {
                var service = value.GetService();
                _services.Add(key, service);
                _sources[service] = value.Sources.Select(x => x.GetSource());
            }
        }

        #region Dictionary Support

        IEnumerator<KeyValuePair<string, IService>> IEnumerable<KeyValuePair<string, IService>>.GetEnumerator()
        {
            return _services.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _services.GetEnumerator();
        }

        int IReadOnlyCollection<KeyValuePair<string, IService>>.Count => _services.Count;

        IEnumerable<string> IReadOnlyDictionary<string, IService>.Keys => _services.Keys;

        IEnumerable<IService> IReadOnlyDictionary<string, IService>.Values => _services.Values;

        IService IReadOnlyDictionary<string, IService>.this[string key] => _services[key];

        bool IReadOnlyDictionary<string, IService>.ContainsKey(string key)
        {
            // Maybe faster? Also may be incorrect...
            return _optionsMonitor.CurrentValue.ContainsKey(key);
        }

        bool IReadOnlyDictionary<string, IService>.TryGetValue(
            string key,
            [MaybeNullWhen(false)] out IService service)
        {
            return _services.TryGetValue(key, out service);
        }

        #endregion
    }
}
