using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Data.Statements;

namespace Cosmos.Dapper.Operations
{
    public partial interface IDapperEntityOperator
    {
        T RunInTransaction<T>(Func<T> func);
        T Get<T>(dynamic id, IDbTransaction transaction, ISQLPredicate[] filters = null) where T : class;
        Task<T> GetAsync<T>(dynamic id, IDbTransaction transaction, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;
        T Get<T>(dynamic id, ISQLPredicate[] filters = null) where T : class;
        Task<T> GetAsync<T>(dynamic id, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;
        void Insert<T>(IEnumerable<T> entities, IDbTransaction transaction) where T : class;
        Task InsertAsync<T>(IEnumerable<T> entities, IDbTransaction transaction, CancellationToken cancellationToken = default) where T : class;
        void Insert<T>(IEnumerable<T> entities) where T : class;
        Task InsertAsync<T>(IEnumerable<T> entities, CancellationToken cancellationToken = default) where T : class;
        dynamic Insert<T>(T entity, IDbTransaction transaction) where T : class;
        Task<dynamic> InsertAsync<T>(T entity, IDbTransaction transaction, CancellationToken cancellationToken = default) where T : class;
        dynamic Insert<T>(T entity) where T : class;
        Task<dynamic> InsertAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class;
        bool Update<T>(T entity, IDbTransaction transaction, ISQLPredicate[] filters = null, bool ignoreAllKeyProperties = false) where T : class;
        Task<bool> UpdateAsync<T>(T entity, IDbTransaction transaction, ISQLPredicate[] filters = null, bool ignoreAllKeyProperties = false, CancellationToken cancellationToken = default) where T : class;
        bool Update<T>(T entity, ISQLPredicate[] filters = null, bool ignoreAllKeyProperties = false) where T : class;
        Task<bool> UpdateAsync<T>(T entity, ISQLPredicate[] filters = null, bool ignoreAllKeyProperties = false, CancellationToken cancellationToken = default) where T : class;
        bool Update<T>(IEnumerable<T> entities, IDbTransaction transaction, ISQLPredicate[] filters = null, bool ignoreAllKeyProperties = false) where T : class;
        Task<bool> UpdateAsync<T>(IEnumerable<T> entities, IDbTransaction transaction, ISQLPredicate[] filters = null, bool ignoreAllKeyProperties = false, CancellationToken cancellationToken = default) where T : class;
        bool Update<T>(IEnumerable<T> entities, ISQLPredicate[] filters = null, bool ignoreAllKeyProperties = false) where T : class;
        Task<bool> UpdateAsync<T>(IEnumerable<T> entities, ISQLPredicate[] filters = null, bool ignoreAllKeyProperties = false, CancellationToken cancellationToken = default) where T : class;
        bool Delete<T>(T entity, IDbTransaction transaction, ISQLPredicate[] filters = null) where T : class;
        Task<bool> DeleteAsync<T>(T entity, IDbTransaction transaction, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;
        bool Delete<T>(T entity, ISQLPredicate[] filters = null) where T : class;
        Task<bool> DeleteAsync<T>(T entity, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;
        bool Delete<T>(IEnumerable<T> entity, IDbTransaction transaction, ISQLPredicate[] filters = null) where T : class;
        Task<bool> DeleteAsync<T>(IEnumerable<T> entity, IDbTransaction transaction, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;
        bool Delete<T>(IEnumerable<T> entity, ISQLPredicate[] filters = null) where T : class;
        Task<bool> DeleteAsync<T>(IEnumerable<T> entity, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;
    }     
}