using System;
using System.Collections.Generic;
using AspectCore.Injector;
using Cosmos.Data.Context;

namespace Cosmos.Data
{
    public class DbContextConfig : DbContextConfigBase<DbContextConfig>, IDbContextConfigureRegister<IServiceContainer>
    {
        private bool DbContextRegistered { get; set; }

        private List<Action<IServiceContainer>> DbContextInitializeActions { get; set; } = new List<Action<IServiceContainer>>();

        internal DbContextConfig(IServiceContainer services)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));

            AddDisposableAction("_dbContextInitializeActionsClear", () =>
            {
                DbContextInitializeActions.Clear();
                DbContextInitializeActions = null;
            });
        }

        public IServiceContainer Services { get; }

        public void RegisterDbContext(Action<IServiceContainer> action)
        {
            if (action == null)
                return;

            DbContextInitializeActions.Add(action);
        }

        internal void ActiveRegister(IServiceContainer services)
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