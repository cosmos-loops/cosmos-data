using System;
using Cosmos.Data;
using Cosmos.Data.Core;
using Cosmos.Data.Internals;
using Cosmos.Data.Store;
using Cosmos.Data.Transaction;
using Cosmos.Data.UnitOfWork;

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
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (!DataSupportFlag.Value)
            {
                services.RegisterInstance(RepositoryManagerFactory.CreateInstance()).SingleInstance();
                services.RegisterType<ScopedRepositoryManager>().InstancePerLifetimeScope();
                services.RegisterType<ScopedStoreContextManager>().As<IStoreContextManager>().InstancePerLifetimeScope();
                services.RegisterType<ScopedTransactionCallingWrapper>().As<ITransactionCallingWrapper>().InstancePerLifetimeScope();
                services.RegisterType<UnitOfWorkManager>().InstancePerLifetimeScope();

                DataSupportFlag.Value = true;
            }
            
            SystemLevelRegister(services);

            if (context != null)
            {
                using (var ctxCfg = new DbContextConfig(services))
                {
                    context(ctxCfg);
                    ctxCfg.ActiveRegister(services);
                }
            }

            return services;
        }

        private static void SystemLevelRegister(ContainerBuilder services)
        {
            var systemLevelActions = SystemSupportRegistrar.GetActionsOnce();

            if (systemLevelActions != null)
            {
                using (var ctxCfg = new DbContextConfig(services))
                {
                    foreach (var action in systemLevelActions)
                    {
                        action?.Invoke(ctxCfg);
                        ctxCfg.ActiveRegister(services);
                    }
                }
            }
        }
    }
}