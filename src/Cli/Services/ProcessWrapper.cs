using System;
using System.Diagnostics;

namespace Cli.Services
{
    /// <summary>
    /// Wraps <see cref="Process"/>.
    /// </summary>
    internal sealed class ProcessWrapper : IProcess
    {
        private readonly Lazy<IProcessStartInfo> _startInfo;
        
        public ProcessWrapper(Process? process = null)
        {
            Process = process ?? new Process();
            _startInfo = new Lazy<IProcessStartInfo>(CreateWrapper);
        }

        public int Id => Process.Id;
        
        public Process Process { get; }

        public IProcessStartInfo StartInfo => _startInfo.Value;

        public bool Start() => Process.Start();

        private ProcessStartInfoWrapper CreateWrapper() => new(Process.StartInfo);
    }
}
