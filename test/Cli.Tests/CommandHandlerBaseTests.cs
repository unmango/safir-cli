using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using System.Threading.Tasks;
using Xunit;

namespace Cli.Tests
{
    public class CommandHandlerBaseTests
    {
        private static readonly InvocationContext _context = new(new RootCommand().Parse());
        
        [Fact]
        public async Task VoidExecute()
        {
            var flag = false;
            var handler = new VoidHandler(() => flag = true);

            await handler.InvokeAsync(_context);
            
            Assert.True(flag);
        }

        private class VoidHandler : CommandHandlerBase
        {
            private readonly Action _execAction;

            public VoidHandler(Action execAction)
            {
                _execAction = execAction;
            }

            public void Execute()
            {
                _execAction();
            }
        }
    }
}
