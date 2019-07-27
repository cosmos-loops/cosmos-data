using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Dapper.Core.SqlKata;
using Cosmos.Data.SqlKata;
using Cosmos.Data.Statements;
using DotNetCore.Collections.Paginable;
using SqlKata.Execution;

namespace Cosmos.Dapper.Store
{
    // ReSharper disable once UnusedTypeParameter
    public abstract partial class StoreBase<TContext, TEntity>
    {

        #region Find by id

        public virtual TEntity FindById(dynamic id)
        {
            return RawTypedContext.EntityOperators.Get<TEntity>(id, RepoLevelDataFilters);
        }

        public virtual Task<TEntity> FindByIdAsync(dynamic id, CancellationToken cancellationToken = default)
        {
            return RawTypedContext.EntityOperators.GetAsync<TEntity>(id, RepoLevelDataFilters, cancellationToken);
        }

        #endregion

        #region Find First, FirstOrDefault, Single, SingleOrDefault by predcate

        public virtual TEntity FindFirst(object predicate, SQLSortSet sort, bool buffered = true)
        {
            return RawTypedContext.EntityOperators.First<TEntity>(predicate, sort, RepoLevelDataFilters, buffered);
        }

        public virtual TEntity FindFirstOrDefault(object predicate, SQLSortSet sort, bool buffered = true)
        {
            return RawTypedContext.EntityOperators.FirstOrDefault<TEntity>(predicate, sort, RepoLevelDataFilters, buffered);
        }

        public virtual TEntity FindSingle(object predicate, SQLSortSet sort, bool buffered = true)
        {
            return RawTypedContext.EntityOperators.Single<TEntity>(predicate, sort, RepoLevelDataFilters, buffered);
        }

        public virtual TEntity FindSingleOrDefault(object predicate, SQLSortSet sort, bool buffered = true)
        {
            return RawTypedContext.EntityOperators.SingleOrDefault<TEntity>(predicate, sort, RepoLevelDataFilters, buffered);
        }

        public virtual Task<TEntity> FindFirstAsync(object predicate, SQLSortSet sort, CancellationToken cancellationToken = default)
        {
            return RawTypedContext.EntityOperators.FirstAsync<TEntity>(predicate, sort, RepoLevelDataFilters, cancellationToken);
        }

        public virtual Task<TEntity> FindFirstOrDefaultAsync(object predicate, SQLSortSet sort, CancellationToken cancellationToken = default)
        {
            return RawTypedContext.EntityOperators.SingleOrDefaultAsync<TEntity>(predicate, sort, RepoLevelDataFilters, cancellationToken);
        }

        public virtual Task<TEntity> FindSingleAsync(object predicate, SQLSortSet sort, CancellationToken cancellationToken = default)
        {
            return RawTypedContext.EntityOperators.SingleAsync<TEntity>(predicate, sort, RepoLevelDataFilters, cancellationToken);
        }

        public virtual Task<TEntity> FindSingleOrDefaultAsync(object predicate, SQLSortSet sort, CancellationToken cancellationToken = default)
        {
            return RawTypedContext.EntityOperators.SingleOrDefaultAsync<TEntity>(predicate, sort, RepoLevelDataFilters, cancellationToken);
        }

        #endregion

        #region Find First, FirstOrDefault, Single, SingleOrDefault by expression

        public virtual TEntity FindFirst(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, bool buffered = true)
        {
            return RawTypedContext.EntityOperators.First(predicate, sort, RepoLevelDataFilters, buffered);
        }

        public virtual TEntity FindFirstOrDefault(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, bool buffered = true)
        {
            return RawTypedContext.EntityOperators.FirstOrDefault(predicate, sort, RepoLevelDataFilters, buffered);
        }

        public virtual TEntity FindSingle(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, bool buffered = true)
        {
            return RawTypedContext.EntityOperators.Single(predicate, sort, RepoLevelDataFilters, buffered);
        }

        public virtual TEntity FindSingleOrDefault(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, bool buffered = true)
        {
            return RawTypedContext.EntityOperators.SingleOrDefault(predicate, sort, RepoLevelDataFilters, buffered);
        }

        public virtual Task<TEntity> FindFirstAsync(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, CancellationToken cancellationToken = default)
        {
            return RawTypedContext.EntityOperators.FirstAsync(predicate, sort, RepoLevelDataFilters, cancellationToken);
        }

        public virtual Task<TEntity> FindFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, CancellationToken cancellationToken = default)
        {
            return RawTypedContext.EntityOperators.SingleOrDefaultAsync(predicate, sort, RepoLevelDataFilters, cancellationToken);
        }

        public virtual Task<TEntity> FindSingleAsync(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, CancellationToken cancellationToken = default)
        {
            return RawTypedContext.EntityOperators.SingleAsync(predicate, sort, RepoLevelDataFilters, cancellationToken);
        }

        public virtual Task<TEntity> FindSingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, CancellationToken cancellationToken = default)
        {
            return RawTypedContext.EntityOperators.SingleOrDefaultAsync(predicate, sort, RepoLevelDataFilters, cancellationToken);
        }

        #endregion
        
        #region Find by predicate

        public virtual IEnumerable<TEntity> FindByPredicate(object predicate, SQLSortSet sort, bool buffered = true)
        {
            return RawTypedContext.EntityOperators.GetList<TEntity>(predicate, sort, RepoLevelDataFilters, buffered);
        }

        public virtual Task<IEnumerable<TEntity>> FindByPredicateAsync(object predicate, SQLSortSet sort, CancellationToken cancellationToken = default)
        {
            return RawTypedContext.EntityOperators.GetListAsync<TEntity>(predicate, sort, RepoLevelDataFilters, cancellationToken);
        }

        public virtual IEnumerable<TEntity> FindByPredicate(object predicate, SQLSortSet sort, int limitFrom, int limitTo, bool buffered = true)
        {
            return RawTypedContext.EntityOperators.GetSet<TEntity>(predicate, sort, limitFrom, limitTo, RepoLevelDataFilters, buffered);
        }

        public virtual Task<IEnumerable<TEntity>> FindByPredicateAsync(object predicate, SQLSortSet sort, int limitFrom, int limitTo, CancellationToken cancellationToken = default)
        {
            return RawTypedContext.EntityOperators.GetSetAsync<TEntity>(predicate, sort, limitFrom, limitTo, RepoLevelDataFilters, cancellationToken);
        }

        public virtual IEnumerable<TEntity> QueryPage(object predicate, SQLSortSet sort, int pageNumber, int pageSize, bool buffered = true)
        {
            return RawTypedContext.EntityOperators.GetPage<TEntity>(predicate, sort, pageNumber, pageSize, RepoLevelDataFilters, buffered);
        }

        public virtual Task<IEnumerable<TEntity>> QueryPageAsync(object predicate, SQLSortSet sort, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            return RawTypedContext.EntityOperators.GetPageAsync<TEntity>(predicate, sort, pageNumber, pageSize, RepoLevelDataFilters, cancellationToken);
        }

        #endregion

        #region Find by SqlKata

        public virtual TEntity FindFirstBySqlKata(Func<QueryBuilder, QueryBuilder> sqlKataFunc)
        {
            return sqlKataFunc.Invoke(RawTypedContext.SqlKataQueryBuilderFunc()())
                .WhereRawSafety(SqlKataRepoLevelDataFilters)
                .QueryFirst<TEntity>();
        }

        public virtual Task<TEntity> FindFirstBySqlKataAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc)
        {
            return sqlKataFunc.Invoke(RawTypedContext.SqlKataQueryBuilderFunc()())
                .WhereRawSafety(SqlKataRepoLevelDataFilters)
                .QueryFirstAsync<TEntity>();
        }

        public virtual TEntity FindFirstOrDefaultBySqlKata(Func<QueryBuilder, QueryBuilder> sqlKataFunc)
        {
            return sqlKataFunc.Invoke(RawTypedContext.SqlKataQueryBuilderFunc()())
                .WhereRawSafety(SqlKataRepoLevelDataFilters)
                .QueryFirstOrDefault<TEntity>();
        }

        public virtual Task<TEntity> FindFirstOrDefaultBySqlKataAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc)
        {
            return sqlKataFunc.Invoke(RawTypedContext.SqlKataQueryBuilderFunc()())
                .WhereRawSafety(SqlKataRepoLevelDataFilters)
                .QueryFirstOrDefaultAsync<TEntity>();
        }

        public virtual TEntity FindSingleBySqlKata(Func<QueryBuilder, QueryBuilder> sqlKataFunc)
        {
            return sqlKataFunc.Invoke(RawTypedContext.SqlKataQueryBuilderFunc()())
                .WhereRawSafety(SqlKataRepoLevelDataFilters)
                .QuerySingle<TEntity>();
        }

        public virtual Task<TEntity> FindSingleBySqlKataAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc)
        {
            return sqlKataFunc.Invoke(RawTypedContext.SqlKataQueryBuilderFunc()())
                .WhereRawSafety(SqlKataRepoLevelDataFilters)
                .QuerySingleAsync<TEntity>();
        }

        public virtual TEntity FindSingleOrDefaultBySqlKata(Func<QueryBuilder, QueryBuilder> sqlKataFunc)
        {
            return sqlKataFunc.Invoke(RawTypedContext.SqlKataQueryBuilderFunc()())
                .WhereRawSafety(SqlKataRepoLevelDataFilters)
                .QuerySingleOrDefault<TEntity>();
        }

        public virtual Task<TEntity> FindSingleOrDefaultBySqlKataAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc)
        {
            return sqlKataFunc.Invoke(RawTypedContext.SqlKataQueryBuilderFunc()())
                .WhereRawSafety(SqlKataRepoLevelDataFilters)
                .QuerySingleOrDefaultAsync<TEntity>();
        }

        public virtual IEnumerable<TEntity> FindBySqlKata(Func<QueryBuilder, QueryBuilder> sqlKataFunc)
        {
            return sqlKataFunc.Invoke(RawTypedContext.SqlKataQueryBuilderFunc()())
                .WhereRawSafety(SqlKataRepoLevelDataFilters)
                .Get<TEntity>();
        }
        
        public virtual IEnumerable<TEntity> FindBySqlKata(Func<QueryBuilder, QueryBuilder> sqlKataFunc, int pageNumber, int pageSize)
        {
            return sqlKataFunc.Invoke(RawTypedContext.SqlKataQueryBuilderFunc()())
                .WhereRawSafety(SqlKataRepoLevelDataFilters)
                .ForPage(pageNumber, pageSize)
                .Get<TEntity>();
        }

        public virtual Task<IEnumerable<TEntity>> FindBySqlKataAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc)
        {
            return sqlKataFunc.Invoke(RawTypedContext.SqlKataQueryBuilderFunc()())
                .WhereRawSafety(SqlKataRepoLevelDataFilters)
                .GetAsync<TEntity>();
        }
        
        public virtual Task<IEnumerable<TEntity>> FindBySqlKataAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc, int pageNumber, int pageSize)
        {
            return sqlKataFunc.Invoke(RawTypedContext.SqlKataQueryBuilderFunc()())
                .WhereRawSafety(SqlKataRepoLevelDataFilters)
                .ForPage(pageNumber, pageSize)
                .GetAsync<TEntity>();
        }
        
        public virtual IPage<TEntity> QueryPage(Func<QueryBuilder, QueryBuilder> sqlKataFunc, int pageNumber, int pageSize)
        {
            return sqlKataFunc.Invoke(RawTypedContext.SqlKataQueryBuilderFunc()())
                .WhereRawSafety(SqlKataRepoLevelDataFilters)
                .GetPage<TEntity>(pageNumber, pageSize);
        }

        public virtual Task<IPage<TEntity>> QueryPageAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc, int pageNumber, int pageSize)
        {
            return sqlKataFunc.Invoke(RawTypedContext.SqlKataQueryBuilderFunc()())
                .WhereRawSafety(SqlKataRepoLevelDataFilters)
                .GetPageAsync<TEntity>(pageNumber, pageSize);
        }

        #endregion

        #region Find by expression

        public virtual IEnumerable<TEntity> FindByPredicate(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, bool buffered = true)
        {
            return RawTypedContext.EntityOperators.GetList(predicate, sort, RepoLevelDataFilters, buffered);
        }

        public virtual Task<IEnumerable<TEntity>> FindByPredicateAsync(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, CancellationToken cancellationToken = default)
        {
            return RawTypedContext.EntityOperators.GetListAsync(predicate, sort, RepoLevelDataFilters, cancellationToken);
        }

        public virtual IEnumerable<TEntity> FindByPredicate(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, int limitFrom, int limitTo, bool buffered = true)
        {
            return RawTypedContext.EntityOperators.GetSet(predicate, sort, limitFrom, limitTo, RepoLevelDataFilters, buffered);
        }

        public virtual Task<IEnumerable<TEntity>> FindByPredicate(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, int limitFrom, int limitTo,
            CancellationToken cancellationToken = default)
        {
            return RawTypedContext.EntityOperators.GetSetAsync(predicate, sort, limitFrom, limitTo, RepoLevelDataFilters, cancellationToken);
        }

        public virtual IEnumerable<TEntity> QueryPage(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, int pageNumber, int pageSize, bool buffered = true)
        {
            return RawTypedContext.EntityOperators.GetPage(predicate, sort, pageNumber, pageSize, RepoLevelDataFilters, buffered);
        }

        public virtual Task<IEnumerable<TEntity>> QueryPageAsync(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, int pageNumber, int pageSize,
            CancellationToken cancellationToken = default)
        {
            return RawTypedContext.EntityOperators.GetPageAsync(predicate, sort, pageNumber, pageSize, RepoLevelDataFilters, cancellationToken);
        }

        #endregion
    }
}