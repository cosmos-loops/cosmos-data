using System;
using Cosmos.Data.Common;
using Cosmos.Dependency;

namespace Autofac
{
    public static class DataRegisterExtensions
    {
        /// <summary>
        /// Add Cosmos Data Service
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static ContainerBuilder RegisterCosmosDataService(this ContainerBuilder builder, Action<DbContextConfig<AutofacProxyRegister>> configure = null)
        {
            using var register = new AutofacProxyRegister(builder);
            register.RegisterDataService(configure);
            return builder;
        }

        /// <summary>
        /// Add repository
        /// </summary>
        /// <param name="builder"></param>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplement"></typeparam>
        /// <returns></returns>
        public static ContainerBuilder RegisterCosmosRepository<TService, TImplement>(this ContainerBuilder builder)
            where TService : IRepository
            where TImplement : class, TService
        {
            using var register = new AutofacProxyRegister(builder);
            register.RegisterRepository<TService, TImplement>();
            return builder;
        }

        /// <summary>
        /// Add repository
        /// </summary>
        /// <param name="builder"></param>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public static ContainerBuilder RegisterCosmosRepository<TService>(this ContainerBuilder builder)
            where TService : class, IRepository
        {
            using var register = new AutofacProxyRegister(builder);
            register.RegisterRepository<TService>();
            return builder;
        }
    }
}