/*
* A copy of https://github.com/2881099/dng.Mysql/blob/master/MySql.Data.MySqlClient/MySqlConnectionPool.cs
* Author: 2881099
* MIT
*/

using System;
using Cosmos.Data.Core.Pools;
using Cosmos.Disposables.ObjectPools;

namespace MySql.Data.MySqlClient
{
    /// <summary>
    /// MySql connection pool
    /// </summary>
    public class MySqlConnectionPool : ObjectPool<MySqlConnection>, IConnectionPool<MySqlConnection>
    {
        internal Action AvailableHandler;
        internal Action UnavailableHandler;

        /// <inheritdoc />
        public MySqlConnectionPool(string name, string connectionString, Action availableHandler, Action unavailableHandler) : base(null)
        {
            var policy = new MySqlConnectionPoolPolicy
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
        public void Recycle(ObjectPayload<MySqlConnection> obj, Exception exception, bool isRecreate = false)
        {
            if (exception is MySqlException)
            {
                try
                {
                    if (obj.Value.Ping() == false)
                        obj.Value.Open();
                }
                catch
                {
                    SetUnavailable(exception);
                }
            }

            base.Recycle(obj, isRecreate);
        }
    }
}