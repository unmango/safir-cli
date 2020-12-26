using System.Threading.Tasks;

namespace Cli.Services
{
    internal interface IServiceRegistry
    {
        Task<IService> GetServiceAsync(string name);
    }
}
