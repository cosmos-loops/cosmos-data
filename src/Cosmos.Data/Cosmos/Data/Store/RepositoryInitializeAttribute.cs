using System;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using AspectCore.Injector;
using Cosmos.IdUtils;

namespace Cosmos.Data.Store
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class RepositoryInitializeAttribute : AbstractInterceptorAttribute
    {
        public override Task Invoke(AspectContext context, AspectDelegate next)
        {
            var instance = context.Implementation;
            var implType = instance.GetType();

            //Get TranceIdAccessor
            var accessor = context.ServiceProvider.Resolve<TraceIdAccessor>();

            //Get repo metadata info from RepositoryManager;
            var manager = context.ServiceProvider.ResolveRequired<RepositoryManager>();
            var metadata = manager.Require(implType);

            //Add metadata info ScopedRepositoryManager;
            var scope = context.ServiceProvider.ResolveRequired<ScopedRepositoryManager>();
            scope.Register(implType, instance, metadata, accessor);

            return next(context);
        }
    }
}