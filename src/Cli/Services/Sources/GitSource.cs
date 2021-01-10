namespace Cli.Services.Sources
{
    internal record GitSource(string CloneUrl) : ServiceSourceBase, IGitSource
    {
    }
}
