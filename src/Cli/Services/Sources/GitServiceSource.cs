using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using SimpleExec;

namespace Cli.Services.Sources
{
    internal class GitServiceSource : IServiceSource
    {
        public ValueTask<CanInitializeResult> CanInitializeAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task InitializeAsync(string workingDirectory, CancellationToken cancellationToken = default)
        {
            // Clone repo
            var args = string.Join(' ', "clone", "TODO: url");
            await Command.RunAsync("git", args, workingDirectory, cancellationToken: cancellationToken);
            
            throw new System.NotImplementedException();
        }

        public bool Satisfies(ServiceEntry service)
        {
            if (service == null) throw new ArgumentNullException(nameof(service));
            if (service.Source != ServiceSource.Git) return false;
            
            throw new System.NotImplementedException();
        }

        private static bool TryGetGitEntry(ServiceEntry service, [MaybeNullWhen(false)] out GitServiceEntry gitService)
        {
            gitService = service as GitServiceEntry;

            return gitService != null;
        }
    }
}
