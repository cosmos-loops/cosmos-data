using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Dapper.Core.SqlKata;
using Cosmos.Data.SqlKata;
using SqlKata.Execution;

namespace Cosmos.Dapper.Store
{
    // ReSharper disable once UnusedTypeParameter
    public abstract partial class StoreBase<TContext, TEntity>
    {
        public virtual void UnsafeRemove(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            RawTypedContext.EntityOperators.Delete(entity, RepoLevelDataFilters);
        }

        public virtual Task UnsafeRemoveAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            return RawTypedContext.EntityOperators.DeleteAsync(entity, RepoLevelDataFilters, cancellationToken);
        }

        public virtual void UnsafeRemove(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));
            RawTypedContext.EntityOperators.Delete(entities, RepoLevelDataFilters);
        }

        public virtual Task UnsafeRemoveAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));
            return RawTypedContext.EntityOperators.DeleteAsync(entities, RepoLevelDataFilters, cancellationToken);
        }

        public virtual int UnsafeRemove(Func<QueryBuilder, QueryBuilder> sqlKataFunc)
        {
            return sqlKataFunc.Invoke(RawTypedContext.SqlKataQueryBuilderFunc()())
                .WhereRawSafety(SqlKataRepoLevelDataFilters)
                .Delete();
        }

        public virtual Task<int> UnsafeRemoveAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc)
        {
            return sqlKataFunc.Invoke(RawTypedContext.SqlKataQueryBuilderFunc()())
                .WhereRawSafety(SqlKataRepoLevelDataFilters)
                .DeleteAsync();
        }
    }
}