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

        public IEnumerator<KeyValuePair<string, ServiceEntry>> GetEnumerator()
        {
            if (Manager != null) yield return new KeyValuePair<string, ServiceEntry>(nameof(Manager), Manager);
            if (Listener != null) yield return new KeyValuePair<string, ServiceEntry>(nameof(Listener), Listener);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int Count => Keys.Count();

        public bool ContainsKey(string key) => Keys.Contains(key);

        public bool TryGetValue(string key, [MaybeNullWhen(false)] out ServiceEntry value)
        {
            value = null;

            using var enumerator = GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Current.Key != key) continue;
                
                value = enumerator.Current.Value;
                return true;
            }

            return false;
        }

        public ServiceEntry this[string key] => throw new NotImplementedException();

        public IEnumerable<string> Keys
        {
            get {
                using var enumerator = GetEnumerator();
                while (enumerator.MoveNext())
                    yield return enumerator.Current.Key;
            }
        }

        public IEnumerable<ServiceEntry> Values
        {
            get {
                using var enumerator = GetEnumerator();
                while (enumerator.MoveNext())
                    yield return enumerator.Current.Value;
            }
        }
    }
}
