using System;
using Cosmos.Data;
using Cosmos.Data.Common;
using Cosmos.Data.Common.Transaction;
using Cosmos.Data.Common.UnitOfWork;
using Cosmos.Data.Core.Internals;
using Cosmos.Data.Core.Pools;
using Cosmos.Data.Core.Registrars;
using Cosmos.Disposables.ObjectPools;

namespace Autofac
{
    /// <summary>
    /// To register Cosmos.Data
    /// </summary>
    public static class RegisterTypesExtensions
    {
        /// <summary>
        /// Add Cosmos Data Support
        /// </summary>
        /// <param name="services"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static ContainerBuilder AddCosmosDataSupport(this ContainerBuilder services, Action<DbContextConfig> context = null)
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services));

            if (!DataSupportFlag.Value)
            {
                services.RegisterInstance(RepositoryManager.Instance).SingleInstance();
                services.RegisterType<ScopedRepositoryManager>().InstancePerLifetimeScope();
                services.RegisterType<ScopedTransactionManager>().As<ITransactionManager>().InstancePerLifetimeScope();
                services.RegisterType<UnitOfWorkManager>().As<IUnitOfWorkManager>().InstancePerLifetimeScope();

                //Register ConnectionPoolManagedModel into ObjectPoolManager
                ObjectPoolManager.Managed<ConnectionPoolManagedModel>.Register();
                
                DataSupportFlag.Value = true;
            }

            SystemLevelRegister(services);

            if (context != null)
            {
                using var ctxCfg = new DbContextConfig(services);
                context(ctxCfg);
                ctxCfg.ActiveRegister(services);
            }

            return services;
        }

        private static void SystemLevelRegister(ContainerBuilder services)
        {
            var systemLevelActions = SystemSupportRegistrar.GetActionsOnce();

            if (systemLevelActions != null)
            {
                using var ctxCfg = new DbContextConfig(services);
                foreach (var action in systemLevelActions)
                {
                    action?.Invoke(ctxCfg);
                    ctxCfg.ActiveRegister(services);
                }
            }
        }
    }
}