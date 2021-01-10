namespace Cli.Services.Sources
{
    internal record LocalDirectorySource(string SourceDirectory)
        : ServiceSourceBase, ILocalDirectorySource
    {
    }
}
