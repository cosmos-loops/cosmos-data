using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Domain.Core;

namespace Cosmos.Dapper.Store
{
    public interface IDynamicExpressionWriteableStore<TEntity> where TEntity : class, IEntity, new()
    {
        bool Remove(Expression<Func<TEntity, bool>> predicate);
        Task<bool> RemoveAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    }
}