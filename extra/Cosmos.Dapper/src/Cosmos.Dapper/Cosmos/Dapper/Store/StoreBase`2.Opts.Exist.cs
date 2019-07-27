using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Data.SqlKata;

namespace Cosmos.Dapper.Store
{
    // ReSharper disable once UnusedTypeParameter
    public abstract partial class StoreBase<TContext, TEntity>
    {
        public bool ExistById(dynamic id)
        {
            return FindById(id) != null;
        }

        public async Task<bool> ExistByIdAsync(dynamic id)
        {
            return await FindByIdAsync(id) != null;
        }

        public bool Exist(object predicate)
        {
            return Count(predicate) > 0;
        }

        public async Task<bool> ExistAsync(object predicate, CancellationToken cancellationToken = default)
        {
            return await CountAsync(predicate, cancellationToken) > 0;
        }

        public bool Exist(Expression<Func<TEntity, bool>> predicate)
        {
            return Count(predicate) > 0;
        }

        public async Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await CountAsync(predicate, cancellationToken) > 0;
        }

        public bool Exist(Func<QueryBuilder, QueryBuilder> sqlKataFunc)
        {
            return Count(sqlKataFunc) > 0;
        }

        public async Task<bool> ExistAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc)
        {
            return await CountAsync(sqlKataFunc) > 0;
        }
    }
}