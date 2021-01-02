using System.Linq;
using Cli.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Cli.Internal
{
    internal static class ServiceOptionsValidation
    {
        public static OptionsBuilder<ServiceOptions> AddValidators(this OptionsBuilder<ServiceOptions> builder)
        {
            // builder.Services.AddSingleton<IValidateOptions<ServiceOptions>, ServiceCount>();

            return builder;
        }
    }
}
