using System.Collections.Generic;
using System.Linq;

namespace Cli.Internal
{
    internal static class PipelineBehaviourExtensions
    {
        public static InvokeAsync<T> BuildPipeline<T>(this IEnumerable<IPipelineBehaviour<T>> behaviours)
            where T : class
            => behaviours.Select(x => x.GetInvokeDelegate()).Aggregate(
                (first, second)
                    => (context, next, cancellationToken)
                        => first(
                            context,
                            innerContext => second(innerContext, next, cancellationToken),
                            cancellationToken));
        
        public static AppliesTo<T> GetAppliesToDelegate<T>(this IPipelineBehaviour<T> behaviour)
            where T : class
            => behaviour.AppliesTo;
        
        public static InvokeAsync<T> GetInvokeDelegate<T>(this IPipelineBehaviour<T> behaviour)
            where T : class
            => behaviour.InvokeAsync;
    }
}
