namespace Cli.Internal
{
    internal static class PipelineBehaviourExtensions
    {
        public static AppliesTo<T> GetAppliesToDelegate<T>(this IPipelineBehaviour<T> behaviour)
            where T : class
            => behaviour.AppliesTo;
        
        public static InvokeAsync<T> GetInvokeDelegate<T>(this IPipelineBehaviour<T> behaviour)
            where T : class
            => behaviour.InvokeAsync;
    }
}
