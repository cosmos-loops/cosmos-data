using System;
using Cosmos.Data;
using Cosmos.Data.Core;
using Cosmos.Data.Internals;
using Cosmos.Data.Store;
using Cosmos.Data.Transaction;
using Cosmos.Data.UnitOfWork;

namespace AspectCore.Injector
{
    /// <summary>
    /// To register Cosmos.Data
    /// </summary>
    public static class AspectCoreInjectorExtensions
    {
        /// <summary>
        /// Add Cosmos Data Support
        /// </summary>
        /// <param name="services"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IServiceContainer AddCosmosDataSupport(this IServiceContainer services, Action<DbContextConfig> context = null)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (!DataSupportFlag.Value)
            {
                services.AddInstance(RepositoryManagerFactory.CreateInstance());
                services.AddType<ScopedRepositoryManager>(Lifetime.Scoped);
                services.AddType<IStoreContextManager, ScopedStoreContextManager>(Lifetime.Scoped);
                services.AddType<ITransactionCallingWrapper, ScopedTransactionCallingWrapper>(Lifetime.Scoped);
                services.AddType<UnitOfWorkManager>(Lifetime.Scoped);

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
        
        private static void SystemLevelRegister(IServiceContainer services)
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
