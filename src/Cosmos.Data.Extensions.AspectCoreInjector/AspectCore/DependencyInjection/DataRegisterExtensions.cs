using System;
using Cosmos.Data.Common;
using Cosmos.Dependency;

namespace AspectCore.DependencyInjection
{
    public static class DataRegisterExtensions
    {
        /// <summary>
        /// Add Cosmos Data Service
        /// </summary>
        /// <param name="context"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IServiceContext AddCosmosDataService(this IServiceContext context, Action<DbContextConfig<AspectCoreProxyRegister>> configure = null)
        {
            using var register = new AspectCoreProxyRegister(context);
            register.RegisterDataService(configure);
            return context;
        }

        /// <summary>
        /// Add repository
        /// </summary>
        /// <param name="context"></param>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplement"></typeparam>
        /// <returns></returns>
        public static IServiceContext AddCosmosRepository<TService, TImplement>(this IServiceContext context)
            where TService : IRepository
            where TImplement : class, TService
        {
            using var register = new AspectCoreProxyRegister(context);
            register.RegisterRepository<TService, TImplement>();
            return context;
        }

        /// <summary>
        /// Add repository
        /// </summary>
        /// <param name="context"></param>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public static IServiceContext AddCosmosRepository<TService>(this IServiceContext context)
            where TService : class, IRepository
        {
            using var register = new AspectCoreProxyRegister(context);
            register.RegisterRepository<TService>();
            return context;
        }
    }
}