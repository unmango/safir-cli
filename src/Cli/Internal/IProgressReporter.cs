using System;

namespace Cli.Internal
{
    public interface IProgressReporter : IDisposable
    {
        void Report(string text);

        void Report(int percentage);

        void Report(float percentage);
    }
}
