using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Domain.Core;
using Cosmos.Validations.Parameters;
using DotNetCore.Collections.Paginable;

namespace Cosmos.Data.Store
{
    public interface IQueriableStore<TEntity, in TKey> where TEntity : class, IEntity<TKey>, new()
    {
        long Count(Expression<Func<TEntity, bool>> predicate);
        Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate);
        bool Exist(TKey id);
        bool Exist(TKey[] ids);
        bool Exist(Expression<Func<TEntity, bool>> predicate);
        Task<bool> ExistAsync(TKey id);
        Task<bool> ExistAsync(TKey[] ids);
        Task<bool> ExistAsync([NotNull] Expression<Func<TEntity, bool>> predicate);
        TEntity FindById(TKey id);
        List<TEntity> FindByIds(params TKey[] ids);
        List<TEntity> FindByIds(IEnumerable<TKey> ids);
        IQueryable<TEntity> Find([NotNull] Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> Find([NotNull] Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize);
        TEntity FindFirstOrDefault([NotNull] Expression<Func<TEntity, bool>> predicate);
        IPage<TEntity> QueryPage([NotNull] Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize);
        Task<TEntity> FindByIdAsync(TKey id, CancellationToken cancellationToken = default);
        Task<List<TEntity>> FindByIdsAsync(params TKey[] ids);
        Task<List<TEntity>> FindByIdsAsync(IEnumerable<TKey> ids, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> FindAsync([NotNull] Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
        Task<TEntity> FindFirstOrDefaultAsync([NotNull] Expression<Func<TEntity, bool>> predicate);
        Task<IPage<TEntity>> QueryPageAsync([NotNull] Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize);
    }
}