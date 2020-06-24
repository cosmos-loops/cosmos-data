using System;
using System.Collections.Generic;
using AspectCore.DependencyInjection;
using Cosmos.Dependency;
using Cosmos.Data.Core.Registrars;

namespace Cosmos.Data
{
    /// <summary>
    /// DbContext config
    /// </summary>
    public class DbContextConfig : DbContextConfigBase<DbContextConfig>, IDbContextConfigureRegister<IServiceContext>
    {
        private bool DbContextRegistered { get; set; }
        private readonly AspectCoreProxyRegister _services;

        private List<Action<IServiceContext>> DbContextInitializeActions { get; set; } = new List<Action<IServiceContext>>();

        internal DbContextConfig(IServiceContext services)
        {
            _services = new AspectCoreProxyRegister(services ?? throw new ArgumentNullException(nameof(services)));

            AddDisposableAction(Core.Constants.DbxClearTaskName, () =>
            {
                DbContextInitializeActions.Clear();
                DbContextInitializeActions = null;
            });
        }

        /// <summary>
        /// Register DbContext
        /// </summary>
        /// <param name="action"></param>
        public void RegisterDbContext(Action<IServiceContext> action)
        {
            if (action is null)
                return;

            DbContextInitializeActions.Add(action);
        }

        internal void ActiveRegister(IServiceContext services)
        {
            if (services is null)
                return;

            if (DbContextRegistered)
                return;

            foreach (var action in DbContextInitializeActions)
            {
                action?.Invoke(services);
            }

            services.RegisterProxyFrom(_services);

            DbContextRegistered = true;
        }

        /// <inheritdoc />
        public override IDbContextConfig Configure(Action<DependencyProxyRegister> configureAction)
        {
            configureAction?.Invoke(_services);
            return this;
        }
    }
}