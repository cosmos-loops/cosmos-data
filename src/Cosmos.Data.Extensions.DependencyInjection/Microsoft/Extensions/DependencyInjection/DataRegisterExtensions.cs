using System;
using Cosmos.Data.Common;
using Cosmos.Dependency;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DataRegisterExtensions
    {
        /// <summary>
        /// Add Cosmos Data Service
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IServiceCollection AddCosmosDataService(this IServiceCollection services, Action<DbContextConfig<MicrosoftProxyRegister>> configure = null)
        {
            using var register = new MicrosoftProxyRegister(services);
            register.RegisterDataService(configure);
            return services;
        }

        /// <summary>
        /// Add repository
        /// </summary>
        /// <param name="services"></param>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplement"></typeparam>
        /// <returns></returns>
        public static IServiceCollection AddCosmosRepository<TService, TImplement>(this IServiceCollection services)
            where TService : IRepository
            where TImplement : class, TService
        {
            using var register = new MicrosoftProxyRegister(services);
            register.RegisterRepository<TService, TImplement>();
            return services;
        }

        /// <summary>
        /// Add repository
        /// </summary>
        /// <param name="services"></param>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public static IServiceCollection AddCosmosRepository<TService>(this IServiceCollection services)
            where TService : class, IRepository
        {
            using var register = new MicrosoftProxyRegister(services);
            register.RegisterRepository<TService>();
            return services;
        }
    }
}