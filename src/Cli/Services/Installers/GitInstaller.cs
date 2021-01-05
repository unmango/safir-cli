using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cli.Services.Installers.Vcs;
using Cli.Services.Sources;
using LibGit2Sharp;

// ReSharper disable NotAccessedField.Local

namespace Cli.Services.Installers
{
    internal class GitInstaller : PipelineServiceInstaller
    {
        private readonly CloneOptions _options = new();
        private readonly string? _cloneUrl;
        private readonly IRepositoryFunctions _repository;

        public GitInstaller() { }
        
        public GitInstaller(string cloneUrl, IRepositoryFunctions repository, IRemoteFunctions remote)
        {
            if (!remote.IsValidName(cloneUrl)) throw new ArgumentException("Invalid clone url", nameof(cloneUrl));

            _cloneUrl = cloneUrl;
            _repository = repository;
        }

        public override bool AppliesTo(InstallationContext context)
        {
            return context.Sources.Any(x => x.TryGetGitSource(out _));
        }

        public override ValueTask InstallAsync(InstallationContext context, CancellationToken cancellationToken = default)
        {
            if (!_repository.IsValid(context.InstallationDirectory))
                _repository.Clone(_cloneUrl, context.InstallationDirectory, _options);

            return ValueTask.CompletedTask;
        }
    }
}
