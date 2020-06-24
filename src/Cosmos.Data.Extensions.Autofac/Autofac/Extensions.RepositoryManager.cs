using Cosmos.Data.Common;

namespace Autofac
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
        public static ContainerBuilder AddRepository<TService, TImplement>(this ContainerBuilder services)
            where TService : IRepository
            where TImplement : class, TService
        {
            var reflector = RepositoryReflector.Create<TService, TImplement>();

            RepositoryManager.Instance.Register(reflector);
            
            services.RegisterType<TImplement>().As<TService>().InstancePerLifetimeScope();

            return services;
        }

        /// <summary>
        /// Add repository
        /// </summary>
        /// <param name="services"></param>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public static ContainerBuilder AddRepository<TService>(this ContainerBuilder services)
            where TService : class, IRepository
        {
            var reflector = RepositoryReflector.Create<TService>();

            RepositoryManager.Instance.Register(reflector);
            
            services.RegisterType<TService>().AsSelf().InstancePerLifetimeScope();
            
            return services;
        }
    }
}