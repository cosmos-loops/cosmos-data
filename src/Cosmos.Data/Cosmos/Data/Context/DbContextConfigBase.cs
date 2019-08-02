using System;
using Cosmos.Dependency;
using Cosmos.Disposables;

namespace Cosmos.Data.Context
{
    /// <summary>
    /// DbContext config base
    /// </summary>
    /// <typeparam name="TConfiguration"></typeparam>
    public abstract class DbContextConfigBase<TConfiguration> : DisposableObjects, IDbContextConfig
        where TConfiguration : class, IDbContextConfig
    {
        /// <summary>
        /// Additional dependency register bag
        /// </summary>
        protected RegisterProxyBag AdditionalDependencyRegisterBag { get; }

        /// <summary>
        /// Create a new instance of DbContextConfigBase
        /// </summary>
        protected DbContextConfigBase()
        {
            AdditionalDependencyRegisterBag = new RegisterProxyBag();
        }

        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="configureAction"></param>
        /// <returns></returns>
        public TConfiguration Configure(Action<TConfiguration> configureAction)
        {
            configureAction?.Invoke(This);

            return This;
        }

        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="configureAction"></param>
        /// <returns></returns>
        public IDbContextConfig Configure(Action<RegisterProxyBag> configureAction)
        {
            configureAction?.Invoke(AdditionalDependencyRegisterBag);

            return this;
        }

        private TConfiguration This => this as TConfiguration;
    }
}