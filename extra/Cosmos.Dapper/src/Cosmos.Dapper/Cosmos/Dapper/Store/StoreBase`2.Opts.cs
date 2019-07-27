using System;
using Cosmos.Dapper.Actions;

namespace Cosmos.Dapper.Store
{
    // ReSharper disable once UnusedTypeParameter
    public abstract partial class StoreBase<TContext, TEntity>
    {
        private readonly Lazy<IDapperSet<TEntity>> _dapperSet;

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public Type EntityType { get; }

        protected string BindingPropertyName { get; }

        #region Dapper action

        private readonly Lazy<ISQLActionEntry<TEntity>> _lazyEntityEntry;
        private readonly Lazy<ISQLActionAsyncEntry<TEntity>> _lazyAsynchronousEntityEntry;

        public ISQLActionEntry<TEntity> EntityEntry => _lazyEntityEntry.Value;

        public ISQLActionAsyncEntry<TEntity> AsynchronousEntityEntry => _lazyAsynchronousEntityEntry.Value;

        #endregion

        #region Dapper Set

        public IDapperSet<TEntity> Set => _dapperSet.Value;

        #endregion
    }
}