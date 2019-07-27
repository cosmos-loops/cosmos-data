using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cosmos.Data.SqlKata;
using Cosmos.Domain.Core;
using DotNetCore.Collections.Paginable;

namespace Cosmos.Dapper.Store
{
    public interface ISqlKataQueryableStore<TEntity> where TEntity : class, IEntity, new()
    {
        TEntity FindFirstBySqlKata(Func<QueryBuilder, QueryBuilder> sqlKataFunc);
        Task<TEntity> FindFirstBySqlKataAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc);
        TEntity FindFirstOrDefaultBySqlKata(Func<QueryBuilder, QueryBuilder> sqlKataFunc);
        Task<TEntity> FindFirstOrDefaultBySqlKataAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc);
        TEntity FindSingleBySqlKata(Func<QueryBuilder, QueryBuilder> sqlKataFunc);
        Task<TEntity> FindSingleBySqlKataAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc);
        TEntity FindSingleOrDefaultBySqlKata(Func<QueryBuilder, QueryBuilder> sqlKataFunc);
        Task<TEntity> FindSingleOrDefaultBySqlKataAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc);
        IEnumerable<TEntity> FindBySqlKata(Func<QueryBuilder, QueryBuilder> sqlKataFunc);
        IEnumerable<TEntity> FindBySqlKata(Func<QueryBuilder, QueryBuilder> sqlKataFunc, int pageNumber, int pageSize);
        Task<IEnumerable<TEntity>> FindBySqlKataAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc);
        Task<IEnumerable<TEntity>> FindBySqlKataAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc, int pageNumber, int pageSize);
        IPage<TEntity> QueryPage(Func<QueryBuilder, QueryBuilder> sqlKataFunc, int pageNumber, int pageSize);
        Task<IPage<TEntity>> QueryPageAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc, int pageNumber, int pageSize);
        int Count(Func<QueryBuilder, QueryBuilder> sqlKataFunc);
        Task<int> CountAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc);
        bool Exist(Func<QueryBuilder, QueryBuilder> sqlKataFunc);
        Task<bool> ExistAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc);
    }
}