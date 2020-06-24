/*
 * A copy of https://github.com/2881099/dng.Pgsql/blob/master/Npgsql/NpgsqlConnectionPool.cs
 * Author: 2881099
 * MIT
 */

using System;
using Cosmos.Data.Core.Pools;
using Cosmos.Disposables.ObjectPools;
using Npgsql;

namespace Cosmos.Data.Sx.Npgsql
{
    /// <summary>
    /// Npgsql connection pool
    /// </summary>
    public class NpgsqlConnectionPool : ObjectPool<NpgsqlConnection>, IConnectionPool<NpgsqlConnection>
    {
        internal Action AvailableHandler;
        internal Action UnavailableHandler;

        /// <inheritdoc />
        public NpgsqlConnectionPool(string name, string connectionString, Action availableHandler, Action unavailableHandler) : base(null)
        {
            var policy = new NpgsqlConnectionPoolPolicy
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
        public void Return(ObjectOut<NpgsqlConnection> obj, Exception exception, bool isRecreate = false)
        {
            if (exception is NpgsqlException)
            {
                if (obj.Value.Ping() == false)
                {
                    SetUnavailable(exception);
                }
            }

            base.Return(obj, isRecreate);
        }
    }
}