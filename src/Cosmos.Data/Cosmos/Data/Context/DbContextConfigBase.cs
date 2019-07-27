using System;
using Cosmos.Dependency;
using Cosmos.Disposables;

namespace Cosmos.Data.Context
{
    public abstract class DbContextConfigBase<TConfiguration> : DisposableObjects, IDbContextConfig
        where TConfiguration : class, IDbContextConfig
    {
        protected RegisterProxyBag AdditionalDependencyRegisterBag { get; }

        protected DbContextConfigBase()
        {
            AdditionalDependencyRegisterBag = new RegisterProxyBag();
        }

        public TConfiguration Configure(Action<TConfiguration> configureAction)
        {
            configureAction?.Invoke(This);

            return This;
        }

        public IDbContextConfig Configure(Action<RegisterProxyBag> configureAction)
        {
            configureAction?.Invoke(AdditionalDependencyRegisterBag);

            return this;
        }

        private TConfiguration This => this as TConfiguration;
    }
}
