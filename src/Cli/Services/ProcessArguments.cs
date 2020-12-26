using System.Diagnostics;

namespace Cli.Services
{
    internal record ProcessArguments(int? Id = null, ProcessStartInfo? StartInfo = null);
}
