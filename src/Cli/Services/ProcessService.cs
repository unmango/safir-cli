using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Cli.Services
{
    internal class ProcessService : IService
    {
        private readonly Config _config;
        private readonly string _process;
        private readonly IReadOnlyList<string> _args;
        
        public ProcessService(IOptions<Config> config, string process, IEnumerable<string> args)
        {
            _config = config.Value;
            _process = process;
            _args = args.ToList();
        }
        
        public Task StartAsync(CancellationToken cancellationToken = default)
        {
            var process = new Process();
            
            throw new System.NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}
