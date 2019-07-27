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
        public virtual void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            RawTypedContext.EntityOperators.Update(entity, RepoLevelDataFilters);
        }

        public virtual void Update(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));
            RawTypedContext.EntityOperators.Update(entities, RepoLevelDataFilters);
        }

        public virtual Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            return RawTypedContext.EntityOperators.UpdateAsync(entity, RepoLevelDataFilters, cancellationToken: cancellationToken);
        }

        public virtual Task UpdateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));
            return RawTypedContext.EntityOperators.UpdateAsync(entities, RepoLevelDataFilters, cancellationToken: cancellationToken);
        }

        public virtual int Update(Func<QueryBuilder, QueryBuilder> sqlKataFunc, object newValues)
        {
            return sqlKataFunc.Invoke(RawTypedContext.SqlKataQueryBuilderFunc()())
                .WhereRawSafety(SqlKataRepoLevelDataFilters)
                .Update(newValues);
        }

        public virtual Task<int> UpdateAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc, object newValues)
        {
            return sqlKataFunc.Invoke(RawTypedContext.SqlKataQueryBuilderFunc()())
                .WhereRawSafety(SqlKataRepoLevelDataFilters)
                .UpdateAsync(newValues);
        }

        public virtual int Update(Func<QueryBuilder, QueryBuilder> sqlKataFunc, IReadOnlyDictionary<string, object> newValues)
        {
            return sqlKataFunc.Invoke(RawTypedContext.SqlKataQueryBuilderFunc()())
                .WhereRawSafety(SqlKataRepoLevelDataFilters)
                .Update(newValues);
        }

        public virtual Task<int> UpdateAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc, IReadOnlyDictionary<string, object> newValues)
        {
            return sqlKataFunc.Invoke(RawTypedContext.SqlKataQueryBuilderFunc()())
                .WhereRawSafety(SqlKataRepoLevelDataFilters)
                .UpdateAsync(newValues);
        }
    }
}