using System;
using Cli.Internal;
using Cli.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Cli.Tests.Services
{
    public class ProcessServiceTests
    {
        private readonly Mock<IProcessFactory> _processFactory = new();
        private readonly Mock<IOptions<Config>> _options = new();
        private readonly Mock<ILogger<ProcessService>> _logger = new();

        private readonly ProcessService _service;

        public ProcessServiceTests()
        {
            _options.SetupGet(x => x.Value).Returns(new Config());
            
            _service = new ProcessService(
                _processFactory.Object,
                _options.Object,
                _logger.Object,
                string.Empty,
                Array.Empty<string>());
        }

        [Fact]
        public void CtorThrowsWhenFactoryIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ProcessService(
                null!,
                _options.Object,
                _logger.Object,
                string.Empty,
                Array.Empty<string>()));
        }

        [Fact]
        public void CtorThrowsWhenOptionsIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ProcessService(
                _processFactory.Object,
                null!,
                _logger.Object,
                string.Empty,
                Array.Empty<string>()));
        }
    }
}
