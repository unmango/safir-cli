using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cli.Services.Configuration;

namespace Cli.Services
{
    internal interface IServiceRegistry : IReadOnlyDictionary<string, IService>
    {
        IEnumerable<IService> Services { get; }

        IEnumerable<IServiceSource> GetSources(IService service);

        IServiceCommand GetCommand(IService service, CommandCategory category);
    }
}
