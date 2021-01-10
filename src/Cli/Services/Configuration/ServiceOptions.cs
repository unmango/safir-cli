using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Cli.Services.Configuration
{
    internal record ServiceOptions : IReadOnlyDictionary<string, ServiceEntry>
    {
        public const string DefaultInstallationDirectory = "services";
        private readonly Dictionary<string, ServiceEntry> _services = new();
        private ServiceEntry? _manager;
        private ServiceEntry? _listener;

        public string? CustomInstallationDirectory { get; init; }

        public ServiceEntry? Manager
        {
            get => _manager;
            set {
                if (value == null) throw new ArgumentNullException(nameof(Manager));
                
                // Maybe won't always want to overwrite a custom name.
                _manager = value with { Name = nameof(Manager) };
            }
        }

        public ServiceEntry? Listener
        {
            get => _listener;
            set {
                if (value == null) throw new ArgumentNullException((nameof(Listener)));

                _listener = value with { Name = nameof(Listener) };
            }
        }

        #region Dictionary Support
        public IEnumerator<KeyValuePair<string, ServiceEntry>> GetEnumerator() => _services.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _services.GetEnumerator();

        public int Count => _services.Count;

        public bool ContainsKey(string key) => _services.ContainsKey(key);

        public bool TryGetValue(string key, [MaybeNullWhen(false)] out ServiceEntry value)
            => _services.TryGetValue(key, out value);

        public ServiceEntry this[string key] => _services[key];

        public IEnumerable<string> Keys => _services.Keys;

        public IEnumerable<ServiceEntry> Values => _services.Values;
        #endregion
    }
}
