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
        public async Task NoExecute()
        {
            var flag = false;
            var handler = TestHandler.Create<NoHandler>(() => flag = true);

            await handler.InvokeAsync(_context);
            
            Assert.False(flag);
        }

        [Fact]
        public async Task VoidExecute()
        {
            var flag = false;
            var handler = TestHandler.Create<VoidHandler>(() => flag = true);

            await handler.InvokeAsync(_context);

            Assert.True(flag);
        }

        [Fact]
        public async Task TaskExecute()
        {
            var flag = false;
            var handler = TestHandler.Create<TaskHandler>(() => flag = true);

            await handler.InvokeAsync(_context);

            Assert.True(flag);
        }

        [Fact]
        public async Task TaskResultExecute()
        {
            var flag = false;
            var handler = TestHandler.Create<TaskResultHandler>(() => flag = true);

            var result = await handler.InvokeAsync(_context);

            Assert.True(flag);
            Assert.Equal(1, result);
        }

        // Also need test for ensuring the correct method is executed
        [Fact]
        public async Task MultipleMethodExecute()
        {
            var count = 0;
            var handler = TestHandler.Create<MultiHandler>(() => count++);

            await handler.InvokeAsync(_context);

            Assert.Equal(1, count);
        }

        private class TestHandler : CommandHandlerBase
        {
            protected Action ExecAction { get; private init; } = null!;

            public static T Create<T>(Action execAction)
                where T : TestHandler, new()
                => new T { ExecAction = execAction };
        }

        private class NoHandler : TestHandler
        {
            public void DoSomething() => ExecAction();
        }

        private class VoidHandler : TestHandler
        {
            public void Execute() => ExecAction();
        }

        private class TaskHandler : TestHandler
        {
            public Task Execute()
            {
                ExecAction();
                return Task.CompletedTask;
            }
        }

        private class TaskResultHandler : TestHandler
        {
            public Task<int> Execute()
            {
                ExecAction();
                return Task.FromResult(1);
            }
        }

        private class MultiHandler : TestHandler
        {
            public void Execute1() => ExecAction();
            
            public void Execute2() => ExecAction();
        }
    }
}
