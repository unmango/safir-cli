using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cli.Services.Installers.Vcs;
using Cli.Services.Sources;
using LibGit2Sharp;

namespace Cli.Services.Installers
{
    internal class GitInstaller : PipelineServiceInstaller
    {
        private readonly CloneOptions _options = new();
        private readonly string? _cloneUrl;
        private readonly IRepositoryFunctions _repository;

        // ReSharper disable once MemberCanBePrivate.Global
        public GitInstaller(IRepositoryFunctions repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        
        internal GitInstaller(string cloneUrl, IRepositoryFunctions repository) : this(repository)
        {
            _cloneUrl = cloneUrl;
        }

        public override bool AppliesTo(InstallationContext context)
        {
            return context.Sources.Any(x => x.TryGetGitSource(out _));
        }

        public override ValueTask InstallAsync(InstallationContext context, CancellationToken cancellationToken = default)
        {
            var (workingDirectory, _, serviceSources) = context;
            var source = serviceSources.Select(x => x.GetGitSource()).First();
            var cloneUrl = _cloneUrl ?? source.CloneUrl;
            
            // TODO: Progress callback?
            if (!_repository.IsValid(workingDirectory))
                _repository.Clone(cloneUrl, workingDirectory, _options);

            return ValueTask.CompletedTask;
        }
    }
}
