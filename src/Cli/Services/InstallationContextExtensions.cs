using System;

namespace Cli.Services
{
    internal static class InstallationContextExtensions
    {
        public static InstallationContext MarkInstalled(this InstallationContext context, ServiceSource source)
            => context.SetInstalled(source);
        
        public static InstallationContext SetInstalled(
            this InstallationContext context,
            ServiceSource source,
            bool installed = true)
        {
            var key = InstalledKey(source);

            return context.WithProperty(key, installed);
        }

        public static bool IsInstalled(this InstallationContext context, ServiceSource source)
            => context.Properties.TryGetKey(InstalledKey(source), out var installed) && (bool)installed;

        public static InstallationContext WithProperty<TKey, TValue>(
            this InstallationContext context,
            TKey key,
            TValue value)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            
            return context with {
                Properties = context.Properties.Add(key, value!)
            };
        }

        private static object InstalledKey(ServiceSource source)
        {
            return (source.Name ?? source.GetType().Name) + "-Installed";
        }
    }
}
