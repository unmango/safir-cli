namespace Cli.Services
{
    internal interface IProcessFactory
    {
        IProcess CreateProcess(ProcessArguments? args = null);
    }
}
