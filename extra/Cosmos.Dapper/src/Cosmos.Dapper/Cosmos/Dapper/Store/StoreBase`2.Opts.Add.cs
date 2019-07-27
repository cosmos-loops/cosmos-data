using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Data.SqlKata;
using SqlKata.Execution;

namespace Cosmos.Dapper.Store
{
    // ReSharper disable once UnusedTypeParameter
    public abstract partial class StoreBase<TContext, TEntity>
    {
        public virtual void Add(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            RawTypedContext.EntityOperators.Insert(entity);
        }

        public virtual void Add(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));
            RawTypedContext.EntityOperators.Insert(entities);
        }

        public virtual Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            return RawTypedContext.EntityOperators.InsertAsync(entity, cancellationToken);
        }

        public virtual Task AddAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));
            return RawTypedContext.EntityOperators.InsertAsync(entities, cancellationToken);
        }

        public virtual int Add(Func<QueryBuilder, QueryBuilder> sqlKataFunc,object data)
        {
            return sqlKataFunc.Invoke(RawTypedContext.SqlKataQueryBuilderFunc()()).Insert(data);
        }
        
        public virtual Task<int> AddAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc,object data)
        {
            return sqlKataFunc.Invoke(RawTypedContext.SqlKataQueryBuilderFunc()()).InsertAsync(data);
        }

        public virtual int Add(Func<QueryBuilder, QueryBuilder> sqlKataFunc, IReadOnlyDictionary<string, object> data)
        {
            return sqlKataFunc.Invoke(RawTypedContext.SqlKataQueryBuilderFunc()()).Insert(data);
        }
        
        public virtual Task<int> AddAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc, IReadOnlyDictionary<string, object> data)
        {
            return sqlKataFunc.Invoke(RawTypedContext.SqlKataQueryBuilderFunc()()).InsertAsync(data);
        }
    }
}