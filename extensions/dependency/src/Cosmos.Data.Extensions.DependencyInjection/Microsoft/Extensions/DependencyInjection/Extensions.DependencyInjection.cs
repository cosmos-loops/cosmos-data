using System;
using Cosmos.Data;
using Cosmos.Data.Context;
using Cosmos.Data.Core;
using Cosmos.Data.Internals;
using Cosmos.Data.Store;
using Cosmos.Data.Transaction;
using Cosmos.Data.UnitOfWork;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// To register Cosmos.Data
    /// </summary>
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddCosmosDataSupport(this IServiceCollection services, Action<DbContextConfig> context = null)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (!DataSupportFlag.Value)
            {
                services.AddSingleton(RepositoryManagerFactory.CreateInstance());
                services.AddScoped<ScopedRepositoryManager>();
                services.AddScoped<IStoreContextManager, ScopedStoreContextManager>();
                services.AddScoped<ITransactionCallingWrapper, ScopedTransactionCallingWrapper>();
                services.AddScoped<UnitOfWorkManager>();

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
        
        private static void SystemLevelRegister(IServiceCollection services)
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
