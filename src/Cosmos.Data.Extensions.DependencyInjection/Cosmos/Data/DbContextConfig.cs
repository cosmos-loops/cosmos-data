using System;
using System.Collections.Generic;
using Cosmos.Data.Core.Registrars;
using Cosmos.Dependency;
using Microsoft.Extensions.DependencyInjection;

namespace Cosmos.Data
{
    /// <summary>
    /// DbContext config
    /// </summary>
    public class DbContextConfig : DbContextConfigBase<DbContextConfig>, IDbContextConfigureRegister<IServiceCollection>
    {
        private bool DbContextRegistered { get; set; }
        private DependencyProxyRegister _services;

        private List<Action<IServiceCollection>> DbContextInitializeActions { get; set; } = new List<Action<IServiceCollection>>();

        internal DbContextConfig(IServiceCollection services)
        {
            _services = new MicrosoftProxyRegister(services ?? throw new ArgumentNullException(nameof(services)));

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
        public void RegisterDbContext(Action<IServiceCollection> action)
        {
            if (action is null)
                return;

            DbContextInitializeActions.Add(action);
        }

        internal void ActiveRegister(IServiceCollection services)
        {
            if (services is null)
                return;

            if (DbContextRegistered)
                return;

            foreach (var action in DbContextInitializeActions)
            {
                action?.Invoke(services);
            }

            services.AddRegisterProxyFrom(_services);

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