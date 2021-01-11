using Cli.Services.Configuration;

namespace Cli.Services
{
    internal static class ServiceEntryExtensions
    {
        public static IService GetService(this ServiceEntry entry)
        {
            return new ServiceEntryWrapper(entry);
        }
    }
}
