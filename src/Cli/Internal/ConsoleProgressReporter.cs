using System;
using System.CommandLine;
using System.Reactive.Subjects;

namespace Cli.Internal
{
    public sealed class ConsoleProgressReporter : IProgressReporter
    {
        private readonly BehaviorSubject<float> _subject = new(0);
        private string _prevLine = string.Empty;

        public ConsoleProgressReporter(IConsole console)
        {
        }

        public void Report(string text)
        {
            var distance = text.Distance(_prevLine);
            
            throw new NotImplementedException();
        }

        public void Report(int percentage)
        {
            throw new NotImplementedException();
        }

        public void Report(float percentage)
        {
            _subject.OnNext(percentage);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
