/*
 * A copy of https://github.com/2881099/dng.Mssql/blob/master/Mssql/SqlConnectionPool.cs
 * Author: 2881099
 * MIT
 */

#if NET452
using Cosmos.Data.Core.Pools;
using Cosmos.Disposables.ObjectPools;

// ReSharper disable once CheckNamespace
namespace System.Data.SqlClient
{
#else
using System;
using Cosmos.Data.Core.Pools;
using Cosmos.Disposables.ObjectPools;

namespace Microsoft.Data.SqlClient
{
#endif

    /// <summary>
    /// SqlConnection Pool 
    /// </summary>
    public class SqlConnectionPool : ObjectPool<SqlConnection>, IConnectionPool<SqlConnection>
    {
        internal Action AvailableHandler;
        internal Action UnavailableHandler;

        /// <inheritdoc />
        public SqlConnectionPool(string name, string connectionString, Action availableHandler, Action unavailableHandler) : base(null)
        {
            var policy = new SqlConnectionPoolPolicy
            {
                _pool = this,
                Name = name
            };

            Policy = policy;
            policy.ConnectionString = connectionString;

            AvailableHandler = availableHandler;
            UnavailableHandler = unavailableHandler;
        }

        /// <summary>
        /// Return
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="exception"></param>
        /// <param name="isRecreate"></param>
        public void Recycle(ObjectPayload<SqlConnection> obj, Exception exception, bool isRecreate = false)
        {
            if (exception is SqlException)
            {
                if (obj.Value.Ping() == false)
                {
                    SetUnavailable(exception);
                }
            }

            base.Recycle(obj, isRecreate);
        }
    }
}