using System;
using System.Linq;
using Cli.Services;
using Xunit;

namespace Cli.Tests.Services
{
    public class SourceTypeValuesExcept : TheoryData<SourceType>
    {
        public SourceTypeValuesExcept(SourceType type)
        {
            var values = Enum.GetValues<SourceType>()
                .Except(new[] { type })
                .Concat(new[] { (SourceType)69 });

            foreach (var value in values) Add(value);
        }
    }
}
