namespace Cli.Services
{
    internal static class ServiceDirectoryExtensions
    {
        public static string GetInstallationDirectory(
            this IServiceDirectory serviceDirectory,
            params string?[] extraPaths)
            => serviceDirectory.GetInstallationDirectory(extraPaths);
    }
}
