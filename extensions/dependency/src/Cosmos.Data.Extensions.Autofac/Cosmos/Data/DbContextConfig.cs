using System;
using System.Collections.Generic;
using Autofac;
using Cosmos.Data.Context;

namespace Cosmos.Data
{
    /// <summary>
    /// DbContext config
    /// </summary>
    public class DbContextConfig : DbContextConfigBase<DbContextConfig>, IDbContextConfigureRegister<ContainerBuilder>
    {
        private bool DbContextRegistered { get; set; }

        private List<Action<ContainerBuilder>> DbContextInitializeActions { get; set; } = new List<Action<ContainerBuilder>>();

        internal DbContextConfig(ContainerBuilder services)
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
        public ContainerBuilder Services { get; }

        /// <summary>
        /// Register DbContext
        /// </summary>
        /// <param name="action"></param>
        public void RegisterDbContext(Action<ContainerBuilder> action)
        {
            if (action == null)
                return;

            DbContextInitializeActions.Add(action);
        }

        internal void ActiveRegister(ContainerBuilder services)
        {
            if (services == null)
                return;

            if (DbContextRegistered)
                return;

            foreach (var action in DbContextInitializeActions)
            {
                action?.Invoke(services);
            }

            services.RegisterProxy(AdditionalDependencyRegisterBag);

            DbContextRegistered = true;
        }
    }
}