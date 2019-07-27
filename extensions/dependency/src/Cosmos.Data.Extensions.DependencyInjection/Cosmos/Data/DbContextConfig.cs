using System;
using System.Collections.Generic;
using Cosmos.Data.Context;
using Microsoft.Extensions.DependencyInjection;

namespace Cosmos.Data
{
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

        public IServiceCollection Services { get; }

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
