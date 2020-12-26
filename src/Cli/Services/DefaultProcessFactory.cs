using System.Diagnostics;

namespace Cli.Services
{
    internal class DefaultProcessFactory : IProcessFactory
    {
        public IProcess CreateProcess(ProcessArguments? args = null)
        {
            if (args == null) return new ProcessWrapper();
            
            Process process = new();

            if (args.Id.HasValue)
            {
                process = Process.GetProcessById(args.Id.Value);
            }

            if (args.StartInfo != null)
            {
                process.StartInfo = args.StartInfo;
            }
            
            return new ProcessWrapper(process);
        }
    }
}
