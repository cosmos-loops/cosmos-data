using Cosmos.Data.Common;

namespace Microsoft.Extensions.DependencyInjection
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
        public static IServiceCollection AddRepository<TService, TImplement>(this IServiceCollection services)
            where TService : class, IRepository
            where TImplement : class, TService
        {
            var reflector = RepositoryReflector.Create<TService, TImplement>();

            RepositoryManager.Instance.Register(reflector);

            services.AddScoped<TService, TImplement>();

            return services;
        }

        /// <summary>
        /// Add repository
        /// </summary>
        /// <param name="services"></param>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public static IServiceCollection AddRepository<TService>(this IServiceCollection services)
            where TService : class, IRepository
        {
            var reflector = RepositoryReflector.Create<TService>();

            RepositoryManager.Instance.Register(reflector);

            services.AddScoped<TService>();

            return services;
        }
    }
}