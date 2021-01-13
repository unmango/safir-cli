using System;
using Cli.Internal.Pipeline;

namespace Cli.Internal.Progress
{
    internal interface IProgressReporter : IPipelineBehaviour<ProgressContext>, IDisposable
    {
        void Report(ProgressContext context);
    }
}
