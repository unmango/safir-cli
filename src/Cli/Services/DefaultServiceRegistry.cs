using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cli.Services
{
    internal class DefaultServiceRegistry : IServiceRegistry
    {
        public Task<IService> GetServiceAsync(string name)
        {
            throw new System.NotImplementedException();
        }
    }
}
