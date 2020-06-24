using System;
using System.Data;
using System.Data.Common;

namespace Cosmos.Data.Common
{
    /// <summary>
    /// An interface of transaction wrapper.
    /// </summary>
    public interface ITransactionWrapper : IUnitOfWorkOperator, IDisposable
    {
        /// <summary>
        /// Transaction Id
        /// </summary>
        string Id { get; }

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
        /// Commit.
        /// </summary>
        /// <param name="clearTransaction">Do need clear transaction after commit.</param>
        void Commit(bool clearTransaction = true);

        /// <summary>
        /// Rollback.
        /// </summary>
        /// <param name="clearTransaction">Do need clear transaction after rollback.</param>
        void Rollback(bool clearTransaction = true);

        /// <summary>
        /// To flag this instance is NullTransactionWrapper or not.
        /// </summary>
        bool IsNull { get; }
    }
}