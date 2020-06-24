using System;
using System.Data;
using System.Data.Common;

namespace Cosmos.Data.Common
{
    /// <summary>
    /// An entry of unit of work.<br />
    /// When the user operates the work unit,
    /// it will operate through this interface.
    /// </summary>
    public interface IUnitOfWorkEntry : IDisposable
    {
        /// <summary>
        /// To return current transaction or begin a new transaction
        /// </summary>
        /// <param name="isCreate"></param>
        /// <returns></returns>
        DbTransaction GetOrBegin(bool isCreate = true);

        /// <summary>
        /// Isolation level
        /// </summary>
        IsolationLevel? IsolationLevel { get; set; }
        
        /// <summary>
        /// Commit
        /// </summary>
        void Commit();
        
        /// <summary>
        /// Rollback
        /// </summary>
        void Rollback();
    }
}