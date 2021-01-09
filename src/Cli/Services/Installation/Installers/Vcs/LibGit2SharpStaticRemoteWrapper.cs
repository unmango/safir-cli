using LibGit2Sharp;

namespace Cli.Services.Installation.Installers.Vcs
{
    public class LibGit2SharpStaticRemoteWrapper : IRemoteFunctions
    {
        public bool IsValidName(string name) => Remote.IsValidName(name);
    }
}
