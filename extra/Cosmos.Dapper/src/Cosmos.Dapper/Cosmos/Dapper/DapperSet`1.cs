using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Dapper.Core;
using Cosmos.Dapper.Operations;
using Cosmos.Data.SqlKata;
using Cosmos.Data.Statements;
using Cosmos.Domain.Core;

namespace Cosmos.Dapper
{
    public class DapperSet<TEntity> : DapperSet, IDapperSet<TEntity> where TEntity : class, IEntity, new()
    {
        private readonly IDapperConnector _connector;
        private readonly IDapperQueryOperator _queryOperator;
        private readonly IDapperCommandOperator _commandOperator;
        private readonly IDapperEntityOperator _entityOperator;
        private readonly IDapperBulkInsertOperator _bulkInsertOperator;
        private Func<QueryBuilder> SqlKataEntityQueryBuilderFunc { get; }

        internal DapperSet(
            IDapperConnector connector,
            IDapperQueryOperator queryOperator,
            IDapperCommandOperator commandOperator,
            IDapperEntityOperator entityOperator,
            IDapperBulkInsertOperator bulkInsertOperator,
            Func<QueryBuilder> sqlKataEntityQueryBuilderFunc)
        {
            _connector = connector ?? throw new ArgumentNullException(nameof(connector));
            _queryOperator = queryOperator ?? throw new ArgumentNullException(nameof(queryOperator));
            _commandOperator = commandOperator ?? throw new ArgumentNullException(nameof(commandOperator));
            _entityOperator = entityOperator ?? throw new ArgumentNullException(nameof(entityOperator));
            _bulkInsertOperator = bulkInsertOperator ?? throw new ArgumentNullException(nameof(bulkInsertOperator));
            SqlKataEntityQueryBuilderFunc = sqlKataEntityQueryBuilderFunc ?? throw new ArgumentNullException(nameof(sqlKataEntityQueryBuilderFunc));
        }

        #region Add

        public void Add(TEntity entity)
        {
            _entityOperator.Insert(entity);
        }

        public Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return _entityOperator.InsertAsync(entity, cancellationToken);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _entityOperator.Insert(entities);
        }

        public Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            return _entityOperator.InsertAsync(entities, cancellationToken);
        }

        #endregion

        #region Update

        public void Update(TEntity entity)
        {
            _entityOperator.Update(entity, RepoLevelDataFilter);
        }

        public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return _entityOperator.UpdateAsync(entity, RepoLevelDataFilter, cancellationToken: cancellationToken);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _entityOperator.Update(entities, RepoLevelDataFilter);
        }

        public Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            return _entityOperator.UpdateAsync(entities, RepoLevelDataFilter, cancellationToken: cancellationToken);
        }

        #endregion

        #region Delete

        public void Delete(TEntity entity)
        {
            _entityOperator.Delete(entity, RepoLevelDataFilter);
        }

        public Task DeleteAsync(TEntity entities, CancellationToken cancellationToken = default)
        {
            return _entityOperator.DeleteAsync(entities, RepoLevelDataFilter, cancellationToken);
        }

        public void DeleteRange(IEnumerable<TEntity> entity)
        {
            _entityOperator.Delete(entity, RepoLevelDataFilter);
        }

        public Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            return _entityOperator.DeleteAsync(entities, RepoLevelDataFilter, cancellationToken);
        }

        public void Delete(ISQLPredicate predicate)
        {
            _entityOperator.Delete<TEntity>(predicate, RepoLevelDataFilter);
        }

        public Task DeleteAsync(ISQLPredicate predicate, CancellationToken cancellationToken = default)
        {
            return _entityOperator.DeleteAsync<TEntity>(predicate, RepoLevelDataFilter, cancellationToken);
        }

        #endregion

        #region Find by id

        public TEntity FindById(dynamic id)
        {
            return _entityOperator.Get<TEntity>(id, RepoLevelDataFilter);
        }

        public Task<TEntity> FindByIdAsync(dynamic id, CancellationToken cancellationToken = default)
        {
            return _entityOperator.GetAsync<TEntity>(id, RepoLevelDataFilter, cancellationToken);
        }

        #endregion

        #region Find by sql

        public TEntity FindSingleBySql(string sql)
        {
            return _queryOperator.QuerySingle<TEntity>(sql);
        }

        public Task<TEntity> FindSingleBySqlAsync(string sql, CancellationToken cancellationToken = default)
        {
            return _queryOperator.QuerySingleAsync<TEntity>(sql, cancellationToken);
        }

        public TEntity FindSingleOrDefaultBySql(string sql)
        {
            return _queryOperator.QuerySingleOrDefault<TEntity>(sql);
        }

        public Task<TEntity> FindSingleOrDefaultBySqlAsync(string sql, CancellationToken cancellationToken = default)
        {
            return _queryOperator.QuerySingleOrDefaultAsync<TEntity>(sql, cancellationToken);
        }

        public TEntity FindFirstBySql(string sql)
        {
            return _queryOperator.QueryFirst<TEntity>(sql);
        }

        public Task<TEntity> FindFirstBySqlAsync(string sql, CancellationToken cancellationToken = default)
        {
            return _queryOperator.QueryFirstAsync<TEntity>(sql, cancellationToken);
        }

        public TEntity FindFirstOrDefaultBySql(string sql)
        {
            return _queryOperator.QueryFirstOrDefault<TEntity>(sql);
        }

        public Task<TEntity> FindFirstOrDefaultBySqlAsync(string sql, CancellationToken cancellationToken = default)
        {
            return _queryOperator.QueryFirstOrDefaultAsync<TEntity>(sql, cancellationToken);
        }

        public IEnumerable<TEntity> FindListBySql(string sql)
        {
            return _queryOperator.Query<TEntity>(sql);
        }

        public Task<IEnumerable<TEntity>> FindListBySqlAsync(string sql, CancellationToken cancellationToken = default)
        {
            return _queryOperator.QueryAsync<TEntity>(sql, cancellationToken);
        }

        #endregion

        #region Fild by SqlKata

        public TEntity FindFirstBySqlKata(Func<QueryBuilder, QueryBuilder> sqlKataFunc)
        {
            return sqlKataFunc(SqlKataEntityQueryBuilderFunc.Invoke()).QueryFirst<TEntity>();
        }

        public Task<TEntity> FindFirstBySqlKataAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc)
        {
            return sqlKataFunc(SqlKataEntityQueryBuilderFunc.Invoke()).QueryFirstAsync<TEntity>();
        }
        
        public TEntity FindFirstOrDefaultBySqlKata(Func<QueryBuilder, QueryBuilder> sqlKataFunc)
        {
            return sqlKataFunc(SqlKataEntityQueryBuilderFunc.Invoke()).QueryFirstOrDefault<TEntity>();
        }

        public Task<TEntity> FindFirstOrDefaultBySqlKataAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc)
        {
            return sqlKataFunc(SqlKataEntityQueryBuilderFunc.Invoke()).QueryFirstOrDefaultAsync<TEntity>();
        }
        
        public TEntity FindSingleBySqlKata(Func<QueryBuilder, QueryBuilder> sqlKataFunc)
        {
            return sqlKataFunc(SqlKataEntityQueryBuilderFunc.Invoke()).QuerySingle<TEntity>();
        }

        public Task<TEntity> FindSingleBySqlKataAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc)
        {
            return sqlKataFunc(SqlKataEntityQueryBuilderFunc.Invoke()).QuerySingleAsync<TEntity>();
        }
        
        public TEntity FindSingleOrDefaultBySqlKata(Func<QueryBuilder, QueryBuilder> sqlKataFunc)
        {
            return sqlKataFunc(SqlKataEntityQueryBuilderFunc.Invoke()).QuerySingleOrDefault<TEntity>();
        }

        public Task<TEntity> FindSingleOrDefaultBySqlKataAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc)
        {
            return sqlKataFunc(SqlKataEntityQueryBuilderFunc.Invoke()).QuerySingleOrDefaultAsync<TEntity>();
        }

        public IEnumerable<TEntity> FindListBySqlKata(Func<QueryBuilder, QueryBuilder> sqlKataFunc)
        {
            return sqlKataFunc(SqlKataEntityQueryBuilderFunc.Invoke()).Query<TEntity>();
        }

        public Task<IEnumerable<TEntity>> FindListBySqlKataAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc)
        {
            return sqlKataFunc(SqlKataEntityQueryBuilderFunc.Invoke()).QueryAsync<TEntity>();
        }

        #endregion

        #region Find by predicate

        public IEnumerable<TEntity> FindListByPredicate(ISQLPredicate predicate, SQLSortSet sort)
        {
            return _entityOperator.GetList<TEntity>(predicate, sort, RepoLevelDataFilter);
        }

        public Task<IEnumerable<TEntity>> FindListByPredicateAsync(ISQLPredicate predicate, SQLSortSet sort, CancellationToken cancellationToken = default)
        {
            return _entityOperator.GetListAsync<TEntity>(predicate, sort, RepoLevelDataFilter, cancellationToken);
        }

        #endregion

        #region Count

        public int Count(ISQLPredicate predicate)
        {
            return _entityOperator.Count<TEntity>(predicate, RepoLevelDataFilter);
        }

        public Task<int> CountAsync(ISQLPredicate predicate, CancellationToken cancellationToken = default)
        {
            return _entityOperator.CountAsync<TEntity>(predicate, RepoLevelDataFilter, cancellationToken);
        }

        #endregion

    }
}