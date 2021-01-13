using System;
using System.CommandLine;

namespace Cli.Internal.Progress
{
    public sealed class ConsoleProgressReporter : IProgressReporter
    {
        private const double Threshold = 69;
        private readonly IConsole _console;
        private string _prevLine = string.Empty;

        public ConsoleProgressReporter(IConsole console)
        {
            _console = console;
        }

        public void Report(string text)
        {
            var similar = text.Distance(_prevLine) >= Threshold;
            // text += text.PadRight(_prevLine.Length);
            _console.Out.Write($"\r{text}");
            _prevLine = text;
        }

        public void Report(int percentage)
        {
            throw new NotImplementedException();
        }

        public void Report(float percentage)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            // No-op for now
        }
    }
}
