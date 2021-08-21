using System;
using Cosmos.Data.Common;
using Cosmos.Data.Common.Transaction;
using Cosmos.Data.Common.UnitOfWork;
using Cosmos.Data.Core.Internals;
using Cosmos.Data.Core.Pools;
using Cosmos.Data.Core.Registrars;
using Cosmos.Disposables.ObjectPools;

namespace Cosmos.Dependency
{
    /// <summary>
    /// Dependency extensions
    /// </summary>
    public static class DependencyExtensions
    {
        /// <summary>
        /// Add Cosmos Data Service
        /// </summary>
        /// <param name="register"></param>
        /// <param name="configure"></param>
        /// <typeparam name="TRegister"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TRegister RegisterDataService<TRegister>(this TRegister register, Action<DbContextConfig<TRegister>> configure = null)
            where TRegister : DependencyProxyRegister
        {
            if (register is null)
                throw new ArgumentNullException(nameof(register));

            if (!DataSupportFlag.Value)
            {
                register.AddSingletonService(RepositoryManager.Instance);
                register.AddScoped<ScopedRepositoryManager>();
                register.AddScoped<ITransactionManager, ScopedTransactionManager>();
                register.AddScoped<IUnitOfWorkManager, UnitOfWorkManager>();

                //Register ConnectionPoolManagedModel into ObjectPoolManager
                ObjectPoolManager.Managed<ConnectionPoolManagedModel>.Register();

                DataSupportFlag.Value = true;
            }
            
            SystemLevelRegister(register);
            
            if (configure is not null)
            {
                using var ctxCfg = new DbContextConfig<TRegister>(register);
                configure(ctxCfg);
                ctxCfg.ActiveRegister(register);
            }

            return register;
        }
        
        private static void SystemLevelRegister<TRegister>(TRegister register)
            where TRegister : DependencyProxyRegister
        {
            var systemLevelActions = SystemSupportRegistrar.GetActionsOnce();

            if (systemLevelActions is not null)
            {
                using var ctxCfg = new DbContextConfig<TRegister>(register);
                foreach (var action in systemLevelActions)
                {
                    action?.Invoke(ctxCfg);
                    ctxCfg.ActiveRegister(register);
                }
            }
        }
    }
}