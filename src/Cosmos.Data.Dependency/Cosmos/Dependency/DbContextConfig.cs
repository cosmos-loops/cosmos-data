using System;
using System.Collections.Generic;
using Cosmos.Data.Core;
using Cosmos.Data.Core.Registrars;

namespace Cosmos.Dependency
{
    public class DbContextConfig<TRegister> : DbContextConfigBase<DbContextConfig<TRegister>>, IDbContextConfigureRegister<TRegister>
        where TRegister : DependencyProxyRegister
    {
        private bool DbContextRegistered { get; set; }
        private readonly TRegister _register;
        
        private List<Action<TRegister>> DbContextInitializeActions { get; set; } = new();

        internal DbContextConfig(TRegister register)
        {
            _register = register ?? throw new ArgumentNullException(nameof(register));

            AddDisposableAction(Constants.DbxClearTaskName, () =>
            {
                DbContextInitializeActions.Clear();
                DbContextInitializeActions = null;
            });
        }
        
        /// <summary>
        /// Register DbContext
        /// </summary>
        /// <param name="action"></param>
        public void RegisterDbContext(Action<TRegister> action)
        {
            if (action is null)
                return;

            DbContextInitializeActions.Add(action);
        }

        internal void ActiveRegister(TRegister register)
        {
            if (register is null)
                return;

            if (DbContextRegistered)
                return;

            foreach (var action in DbContextInitializeActions)
            {
                action?.Invoke(register);
            }

            DbContextRegistered = true;
        }

        /// <inheritdoc />
        public override IDbContextConfig Configure(Action<DependencyProxyRegister> configureAction)
        {
            configureAction?.Invoke(_register);
            return this;
        }
    }
}