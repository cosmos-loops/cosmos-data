using System;
using System.Data;
using Cosmos.Disposables.ObjectPools;

namespace Cosmos.Data.Core.Pools
{
    /// <summary>
    /// Connection pool, an accessor for <see cref="ObjectPoolManager"/>.
    /// </summary>
    public static class ConnectionPool
    {
        /// <summary>
        /// Get connection
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="timeout"></param>
        /// <typeparam name="TConn"></typeparam>
        /// <returns></returns>
        public static ObjectOut<TConn> Get<TConn>(string connectionString, TimeSpan? timeout = null)
            where TConn : IDbConnection
        {
            return Pools.Get<TConn>(connectionString).Get(timeout);
        }

        /// <summary>
        /// Return connection
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="connectionString"></param>
        /// <typeparam name="TConn"></typeparam>
        public static void Return<TConn>(ObjectOut<TConn> obj, string connectionString)
            where TConn : IDbConnection
        {
            Pools.Get<TConn>(connectionString).Return(obj);
        }

        /// <summary>
        /// Pools
        /// </summary>
        public static class Pools
        {
            /// <summary>
            /// Get connection pool
            /// </summary>
            /// <param name="connectionString"></param>
            /// <typeparam name="TConn"></typeparam>
            /// <returns></returns>
            public static IObjectPool<TConn> Get<TConn>(string connectionString) where TConn : IDbConnection
            {
                return ObjectPoolManager.Managed<ConnectionPoolManagedModel>.Get<TConn>(connectionString);
            }

            /// <summary>
            /// Register<br />
            /// 注册指定类型和指定连接字符串的数据库连接池
            /// </summary>
            /// <param name="poolFunc"></param>
            /// <param name="connectionString"></param>
            /// <typeparam name="TConn"></typeparam>
            /// <typeparam name="TPool"></typeparam>
            public static void Register<TConn, TPool>(Func<TPool> poolFunc, string connectionString)
                where TConn : IDbConnection
                where TPool : class, IObjectPool<TConn>
            {
                if (!ObjectPoolManager.Managed<ConnectionPoolManagedModel>.Contains(typeof(TConn), connectionString))
                {
                    if (poolFunc is null)
                        throw new ArgumentNullException(nameof(poolFunc));
                    ObjectPoolManager.Managed<ConnectionPoolManagedModel>.Create(connectionString, poolFunc());
                }
            }
        }
    }
}