using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Data.SqlKata;
using Cosmos.Domain.Core;

namespace Cosmos.Dapper.Store
{
    public interface IWriteableStore<in TEntity> where TEntity : class, IEntity, new()
    {
        void Add(TEntity entity);
        void Add(IEnumerable<TEntity> entities);
        int Add(Func<QueryBuilder, QueryBuilder> sqlKataFunc, object data);
        int Add(Func<QueryBuilder, QueryBuilder> sqlKataFunc, IReadOnlyDictionary<string, object> data);
        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task AddAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        Task<int> AddAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc, object data);
        Task<int> AddAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc, IReadOnlyDictionary<string, object> data);
        void Update(TEntity entity);
        void Update(IEnumerable<TEntity> entities);
        int Update(Func<QueryBuilder, QueryBuilder> sqlKataFunc, object newValues);
        int Update(Func<QueryBuilder, QueryBuilder> sqlKataFunc, IReadOnlyDictionary<string, object> newValues);
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        Task<int> UpdateAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc, object newValues);
        Task<int> UpdateAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc, IReadOnlyDictionary<string, object> newValues);
        void Remove(TEntity entity);
        void Remove(IEnumerable<TEntity> entities);
        Task RemoveAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task RemoveAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        void UnsafeRemove(TEntity entity);
        void UnsafeRemove(IEnumerable<TEntity> entities);
        int UnsafeRemove(Func<QueryBuilder, QueryBuilder> sqlKataFunc);
        Task UnsafeRemoveAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task UnsafeRemoveAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        Task<int> UnsafeRemoveAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc);
    }
}