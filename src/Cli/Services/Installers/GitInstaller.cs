using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cli.Services.Sources;
using LibGit2Sharp;

// ReSharper disable NotAccessedField.Local

namespace Cli.Services.Installers
{
    internal class GitInstaller : PipelineServiceInstaller
    {
        private readonly CloneOptions _options = new();
        private readonly string? _cloneUrl;
        private readonly IRepository _repository;

        public GitInstaller() { }
        
        public GitInstaller(string cloneUrl, IRepository repository)
        {
            _cloneUrl = cloneUrl;
            _repository = repository;
        }

        public override bool AppliesTo(InstallationContext context)
        {
            return context.Sources.Any(x => x.TryGetGitSource(out _));
        }

        public override ValueTask InstallAsync(InstallationContext context, CancellationToken cancellationToken = default)
        {
            if (!Repository.IsValid(context.InstallationDirectory))
                Repository.Clone(_cloneUrl, context.InstallationDirectory);
            
            return ValueTask.CompletedTask;
        }
    }
}
