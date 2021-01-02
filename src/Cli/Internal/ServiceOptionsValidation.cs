using System.Collections.Generic;
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
            builder.Services.AddSingleton<IValidateOptions<ServiceOptions>, ServiceCount>();

            return builder;
        }

        private class ServiceCount : IValidateOptions<ServiceOptions>
        {
            public ValidateOptionsResult Validate(string name, ServiceOptions options)
            {
                var duplicates = options.Values.GroupBy(x => x.Service).Where(x => x.Count() > 1).ToList();

                return duplicates.Any()
                    ? ValidateOptionsResult.Fail(duplicates.Select(Message))
                    : ValidateOptionsResult.Success;
            }

            private static string Message(IGrouping<ServiceImplementation, ServiceEntry> duplicate)
            {
                return
                    $"Duplicate entries for service {duplicate.Key}: {string.Join(", ", duplicate.Select(x => x.Name))}";
            }
        }
    }
}
