using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Data.Statements;

// ReSharper disable InconsistentNaming

namespace Cosmos.Dapper.Core
{
    public partial interface IDapperImplementor
    {
        ISQLGenerator SQLGenerator { get; }
        T Get<T>(IDbConnection connection, dynamic id, IDbTransaction transaction, ISQLPredicate[] filters = null) where T : class;
        Task<T> GetAsync<T>(IDbConnection connection, dynamic id, IDbTransaction transaction, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;
        void Insert<T>(IDbConnection connection, IEnumerable<T> entities, IDbTransaction transaction) where T : class;
        dynamic Insert<T>(IDbConnection connection, T entity, IDbTransaction transaction) where T : class;
        Task InsertAsync<T>(IDbConnection connection, IEnumerable<T> entities, IDbTransaction transaction = null, CancellationToken cancellationToken = default) where T : class;
        Task<dynamic> InsertAsync<T>(IDbConnection connection, T entity, IDbTransaction transaction = null, CancellationToken cancellationToken = default) where T : class;
        bool Update<T>(IDbConnection connection, T entity, IDbTransaction transaction, ISQLPredicate[] filters = null, bool ignoreAllKeyProperties = false) where T : class;
        Task<bool> UpdateAsync<T>(IDbConnection connection, T entity, IDbTransaction transaction, ISQLPredicate[] filters = null, bool ignoreAllKeyProperties = false, CancellationToken cancellationToken = default) where T : class;
        bool Update<T>(IDbConnection connection, IEnumerable<T> entities, IDbTransaction transaction, ISQLPredicate[] filters = null, bool ignoreAllKeyProperties = false) where T : class;
        Task<bool> UpdateAsync<T>(IDbConnection connection, IEnumerable<T> entities, IDbTransaction transaction, ISQLPredicate[] filters = null, bool ignoreAllKeyProperties = false, CancellationToken cancellationToken = default) where T : class;
        bool Delete<T>(IDbConnection connection, T entity, IDbTransaction transaction, ISQLPredicate[] filters = null) where T : class;
        bool Delete<T>(IDbConnection connection, IEnumerable<T> entities, IDbTransaction transaction, ISQLPredicate[] filters = null) where T : class;
        Task<bool> DeleteAsync<T>(IDbConnection connection, T entity, IDbTransaction transaction, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;
        Task<bool> DeleteAsync<T>(IDbConnection connection, IEnumerable<T> entities, IDbTransaction transaction, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;
   }
}