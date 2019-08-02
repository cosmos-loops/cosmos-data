using System;
using System.Collections.Generic;
using Cosmos.Data.Context;
using Microsoft.Extensions.DependencyInjection;

namespace Cosmos.Data
{
    /// <summary>
    /// DbContext config
    /// </summary>
    public class DbContextConfig : DbContextConfigBase<DbContextConfig>, IDbContextConfigureRegister<IServiceCollection>
    {
        private bool DbContextRegistered { get; set; }

        private List<Action<IServiceCollection>> DbContextInitializeActions { get; set; } = new List<Action<IServiceCollection>>();

        internal DbContextConfig(IServiceCollection services)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));

            AddDisposableAction("_dbContextInitializeActionsClear", () =>
            {
                DbContextInitializeActions.Clear();
                DbContextInitializeActions = null;
            });
        }

        /// <summary>
        /// Gets services
        /// </summary>
        public IServiceCollection Services { get; }

        /// <summary>
        /// Register DbContext
        /// </summary>
        /// <param name="action"></param>
        public void RegisterDbContext(Action<IServiceCollection> action)
        {
            if (action == null)
                return;

            DbContextInitializeActions.Add(action);
        }

        internal void ActiveRegister(IServiceCollection services)
        {
            if (services == null)
                return;

            if (DbContextRegistered)
                return;

            foreach (var action in DbContextInitializeActions)
            {
                action?.Invoke(services);
            }

            services.AddRegisterProxy(AdditionalDependencyRegisterBag);

            DbContextRegistered = true;
        }
    }
}
