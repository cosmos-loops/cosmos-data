using System;
using System.Reflection;
using System.Threading.Tasks;
using AspectCore.DependencyInjection;
using AspectCore.DynamicProxy;
using Cosmos.IdUtils;
using Cosmos.Reflection;

namespace Cosmos.Data.Common
{
    /// <summary>
    /// Repository initializing interceptor
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface)]
    public class RepositoryLifecycleAttribute : AbstractInterceptorAttribute
    {
        /// <inheritdoc />
        public override Task Invoke(AspectContext context, AspectDelegate next)
        {
            var instance = context.Implementation;
            var implementType = instance.GetType();

            if (!(instance is IRepository repository))
                throw new ArgumentException("Cannot convert instance from object to IRepository.");

            //Get TranceIdAccessor
            var accessor = context.ServiceProvider.Resolve<TraceIdAccessor>();

            //Get repo metadata info from RepositoryManager;
            var manager = context.ServiceProvider.ResolveRequired<RepositoryManager>();
            var metadata = manager.Require(implementType);

            //Add metadata info ScopedRepositoryManager;
            var scope = context.ServiceProvider.ResolveRequired<ScopedRepositoryManager>();
            var runtimeScopedMetadata = metadata.RuntimeScoped(repository, accessor);

            scope.Register(implementType, runtimeScopedMetadata);
            context.Implementation.GetValueAccessor().SetValue("CurrentTraceId", runtimeScopedMetadata.TranceId);

            return next(context);
        }
    }
}