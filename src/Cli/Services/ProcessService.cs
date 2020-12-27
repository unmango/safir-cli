using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Cli.Services
{
    internal class ProcessService : IService
    {
        private readonly IProcessFactory _processFactory;
        private readonly Config _config;
        private readonly ILogger<ProcessService> _logger;
        private readonly string _process;
        private readonly IReadOnlyList<string> _args;

        public ProcessService(
            IProcessFactory processFactory,
            IOptions<Config> config,
            ILogger<ProcessService> logger,
            string process,
            IEnumerable<string> args)
        {
            _processFactory = processFactory ?? throw new ArgumentNullException(nameof(processFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _config = config?.Value ?? throw new ArgumentNullException(nameof(config));
            _process = process;
            _args = args.ToList();
        }

        public Task StartAsync(CancellationToken cancellationToken = default)
        {
            _logger.StartInvoked(GetType());
            
            var process = _processFactory.Create(startInfo => {
                startInfo.FileName = _process;
                startInfo.Arguments = string.Join(' ', _args);
            });
            
            _logger.ProcessCreated(process.Id);

            var result = process.Start();
            
            _logger.ProcessStarted(process.Id, result);

            return Task.FromResult(result ? 0 : 1);
        }

        public Task StopAsync(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}
