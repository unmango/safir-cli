using Cli.Services.Installation.Installers;
using Cli.Services.Installation.Installers.Vcs;
using Microsoft.Extensions.DependencyInjection;

namespace Cli.Services
{
    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceInstallationPipeline(this IServiceCollection services)
        {
            services.AddLogging();
            
            services.AddTransient<IInstallationPipeline, DefaultInstallationPipeline>();

            services.AddLibGit2Sharp();
            services.AddTransient<IInstallationMiddleware, GitInstaller>();
            
            return services;
        }
    }
}
