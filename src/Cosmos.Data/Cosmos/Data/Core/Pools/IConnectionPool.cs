using System;
using System.Data;
using System.Threading.Tasks;
using Cosmos.Disposables.ObjectPools;
using Cosmos.Disposables.ObjectPools.Core;
using Cosmos.Disposables.ObjectPools.Statistics;

namespace Cosmos.Data.Core.Pools
{
    /// <summary>
    /// Interface of connection pool
    /// </summary>
    public interface IConnectionPool
    {
        /// <summary>
        /// Statistics
        /// </summary>
        StatisticsInfo GetStatisticsInfo();

        /// <summary>
        /// StatisticsFully
        /// </summary>
        FullStatisticsInfo GetStatisticsInfoFully();
    }

    /// <summary>
    /// Interface of connection pool
    /// </summary>
    /// <typeparam name="TConn"></typeparam>
    public interface IConnectionPool<TConn> : IConnectionPool
        where TConn : IDbConnection
    {
        /// <summary>
        /// Get
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns></returns>
        ObjectPayload<TConn> Acquire(TimeSpan? timeout = null);

        /// <summary>
        /// Get async
        /// </summary>
        /// <returns></returns>
        Task<ObjectPayload<TConn>> AcquireAsync();

        /// <summary>
        /// Return
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="isReset"></param>
        void Recycle(ObjectPayload<TConn> obj, bool isReset = false);

        /// <summary>
        /// Return
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="exception"></param>
        /// <param name="isRecreate"></param>
        void Recycle(ObjectPayload<TConn> obj, Exception exception, bool isRecreate = false);

        /// <summary>
        /// Dispose
        /// </summary>
        void Dispose();
    }
}