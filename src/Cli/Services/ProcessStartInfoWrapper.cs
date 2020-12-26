using System.Diagnostics;

namespace Cli.Services
{
    internal sealed class ProcessStartInfoWrapper : IProcessStartInfo
    {
        public ProcessStartInfoWrapper(ProcessStartInfo? startInfo = null)
        {
            StartInfo = startInfo ?? new ProcessStartInfo();
        }
        
        public ProcessStartInfo StartInfo { get; }
    }
}
