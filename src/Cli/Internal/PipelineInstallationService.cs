using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cli.Services;
using Microsoft.Extensions.Logging;

namespace Cli.Internal
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class PipelineInstallationService : IInstallationService
    {
        private static readonly Func<InstallationContext, CancellationToken, ValueTask> _seed =
            (_, _) => ValueTask.CompletedTask;

        private readonly IServiceDirectory _serviceDirectory;
        private readonly IEnumerable<IPipelineServiceInstaller> _installers;
        private readonly ILogger<PipelineInstallationService> _logger;

        public PipelineInstallationService(
            IServiceDirectory serviceDirectory,
            IEnumerable<IPipelineServiceInstaller> installers,
            ILogger<PipelineInstallationService> logger)
        {
            _serviceDirectory = serviceDirectory ?? throw new ArgumentNullException(nameof(serviceDirectory));
            _installers = installers ?? throw new ArgumentNullException(nameof(installers));
            _logger = logger;
        }

        public async Task InstallAsync(
            ServiceEntry service,
            string? directory = null,
            CancellationToken cancellationToken = default)
        {
            var workingDirectory = _serviceDirectory.GetInstallationDirectory(directory);

            // ReSharper disable once UnusedVariable
            var context = new InstallationContext(
                workingDirectory,
                service,
                // TODO: Select sources
                service.Sources);

            var pipeline = CreatePipeline(_installers);
            await pipeline(context, cancellationToken);
        }

        private static Func<InstallationContext, CancellationToken, ValueTask> CreatePipeline(
            IEnumerable<IPipelineServiceInstaller> installers)
            => installers.Aggregate(
                _seed,
                (pipeline, installer)
                    => (context, cancellationToken)
                        => installer.InvokeAsync(
                            context,
                            innerContext => pipeline(innerContext, cancellationToken),
                            cancellationToken));
    }
}
