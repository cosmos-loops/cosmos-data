using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Dapper.Core.Contextual;
using Cosmos.Data.Store;
using Cosmos.Domain.Core;

namespace Cosmos.Dapper.Store
{
    public abstract class StoreBase<TContext, TEntity, TKey> : StoreBase<TContext, TEntity>
        where TContext : class, IDapperContext, IStoreContext, IWithSQLGenerator
        where TEntity : class, IEntity, new()
    {
        protected StoreBase(TContext context, Expression<Func<TContext, IDapperSet<TEntity>>> bindingExpression, bool includeUnsafeOpt)
            : base(context, bindingExpression, includeUnsafeOpt)
        {
            KeyType = typeof(TKey);
        }

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public Type KeyType { get; }

        #region Find

        public virtual TEntity FindById(TKey id)
        {
            return RawTypedContext.EntityOperators.Get<TEntity>(id, RepoLevelDataFilters);
        }

        public virtual Task<TEntity> FindByIdAsync(TKey id, CancellationToken cancellationToken = default)
        {
            return RawTypedContext.EntityOperators.GetAsync<TEntity>(id, RepoLevelDataFilters, cancellationToken);
        }

        #endregion

        #region Exist

        public bool ExistById(TKey id)
        {
            return FindById(id) != null;
        }

        public async Task<bool> ExistByIdAsync(TKey id, CancellationToken cancellationToken = default)
        {
            return await FindByIdAsync(id, cancellationToken) != null;
        }

        #endregion

    }
}