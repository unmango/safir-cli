using System;
using System.Diagnostics;

namespace Cli.Services
{
    internal static class ProcessFactoryExtensions
    {
        public static IProcess Create(this IProcessFactory factory, Action<ProcessStartInfo> configure)
        {
            if (configure == null) throw new ArgumentNullException(nameof(configure));
            
            var startInfo = new ProcessStartInfo();

            configure(startInfo);

            return factory.Create(new ProcessArguments(null, startInfo));
        }

        public static IProcess Create(this IProcessFactory factory, int id)
            => factory.Create(new ProcessArguments(id));

        public static IProcess Create(this IProcessFactory factory, ProcessArguments? args = null)
            => factory?.CreateProcess(args) ?? throw new ArgumentNullException(nameof(factory));
    }
}
