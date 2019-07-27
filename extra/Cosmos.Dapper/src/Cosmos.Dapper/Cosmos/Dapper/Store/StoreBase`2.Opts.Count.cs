using System;
using System.Linq;
using System.Linq.Expressions;
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
        public int Count(object predicate)
        {
            return RawTypedContext.EntityOperators.GetList<TEntity>(predicate, null, RepoLevelDataFilters).Count();
        }

        public async Task<int> CountAsync(object predicate, CancellationToken cancellationToken = default)
        {
            return (await RawTypedContext.EntityOperators.GetListAsync<TEntity>(predicate, null, RepoLevelDataFilters, cancellationToken)).Count();
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return RawTypedContext.EntityOperators.GetList(predicate, null, RepoLevelDataFilters).Count();
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return (await RawTypedContext.EntityOperators.GetListAsync(predicate, null, RepoLevelDataFilters, cancellationToken)).Count();
        }

        public int Count(Func<QueryBuilder, QueryBuilder> sqlKataFunc)
        {
            return sqlKataFunc.Invoke(RawTypedContext.SqlKataQueryBuilderFunc()()).WhereRawSafety(SqlKataRepoLevelDataFilters).Count<int>();
        }
        
        public Task<int> CountAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc)
        {
            return sqlKataFunc.Invoke(RawTypedContext.SqlKataQueryBuilderFunc()()).WhereRawSafety(SqlKataRepoLevelDataFilters).CountAsync<int>();
        }
    }
}