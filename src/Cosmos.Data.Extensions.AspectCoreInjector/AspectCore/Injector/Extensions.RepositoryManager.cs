using AspectCore.DependencyInjection;
using Cosmos.Data.Common;

namespace AspectCore.Injector
{
    /// <summary>
    /// To register repository of Cosmos.Data
    /// </summary>
    public static class RepositoryManagerExtensions
    {
        /// <summary>
        /// Add repository
        /// </summary>
        /// <param name="services"></param>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplement"></typeparam>
        /// <returns></returns>
        public static IServiceContext AddRepository<TService, TImplement>(this IServiceContext services)
            where TService : IRepository
            where TImplement : class, TService
        {
            var reflector = RepositoryReflector.Create<TService, TImplement>();

            RepositoryManager.Instance.Register(reflector);
            
            services.AddType<TService, TImplement>(Lifetime.Scoped);

            return services;
        }

        /// <summary>
        /// Add repository
        /// </summary>
        /// <param name="services"></param>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public static IServiceContext AddRepository<TService>(this IServiceContext services)
            where TService :class, IRepository
        {
            var reflector = RepositoryReflector.Create<TService>();

            RepositoryManager.Instance.Register(reflector);
            
            services.AddType<TService>(Lifetime.Scoped);

            return services;
        }
    }
}