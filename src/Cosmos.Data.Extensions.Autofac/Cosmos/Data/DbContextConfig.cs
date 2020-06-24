using System;
using System.Collections.Generic;
using Autofac;
using Cosmos.Data.Core.Registrars;
using Cosmos.Dependency;

namespace Cosmos.Data
{
    /// <summary>
    /// DbContext config
    /// </summary>
    public class DbContextConfig : DbContextConfigBase<DbContextConfig>, IDbContextConfigureRegister<ContainerBuilder>
    {
        private bool DbContextRegistered { get; set; }
        private DependencyProxyRegister _services;

        private List<Action<ContainerBuilder>> DbContextInitializeActions { get; set; } = new List<Action<ContainerBuilder>>();

        internal DbContextConfig(ContainerBuilder services)
        {
            _services = new AutofacProxyRegister(services ?? throw new ArgumentNullException(nameof(services)));

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
        public void RegisterDbContext(Action<ContainerBuilder> action)
        {
            if (action is null)
                return;

            DbContextInitializeActions.Add(action);
        }

        internal void ActiveRegister(ContainerBuilder services)
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