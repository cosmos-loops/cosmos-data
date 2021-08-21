using System;
using Cosmos.Dependency;
using Cosmos.Disposables;

namespace Cosmos.Data.Core.Registrars
{
    /// <summary>
    /// DbContext config base
    /// </summary>
    /// <typeparam name="TConfiguration"></typeparam>
    public abstract class DbContextConfigBase<TConfiguration> : DisposableObjects, IDbContextConfig
        where TConfiguration : class, IDbContextConfig
    {
        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="configureAction"></param>
        /// <returns></returns>
        public virtual TConfiguration Configure(Action<TConfiguration> configureAction)
        {
            configureAction?.Invoke(This);
            return This;
        }

        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="configureAction"></param>
        /// <returns></returns>
        public abstract IDbContextConfig Configure(Action<DependencyProxyRegister> configureAction);

        private TConfiguration This => this as TConfiguration;
    }
}