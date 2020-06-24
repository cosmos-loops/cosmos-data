using System;
using System.Data;
using System.Threading.Tasks;
using Cosmos.Disposables.ObjectPools;

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
        string Statistics { get; }

        /// <summary>
        /// StatisticsFully
        /// </summary>
        string StatisticsFully { get; }
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
        ObjectOut<TConn> Get(TimeSpan? timeout = null);

        /// <summary>
        /// Get async
        /// </summary>
        /// <returns></returns>
        Task<ObjectOut<TConn>> GetAsync();

        /// <summary>
        /// Return
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="isReset"></param>
        void Return(ObjectOut<TConn> obj, bool isReset = false);

        /// <summary>
        /// Return
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="exception"></param>
        /// <param name="isRecreate"></param>
        void Return(ObjectOut<TConn> obj, Exception exception, bool isRecreate = false);

        /// <summary>
        /// Dispose
        /// </summary>
        void Dispose();
    }
}