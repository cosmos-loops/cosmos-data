using System;
using System.Data;

namespace Cosmos.Data.Transaction
{
    /// <summary>
    /// An interface of transaction wrapper.
    /// </summary>
    public interface ITransactionWrapper : IDisposable
    {
        /// <summary>
        /// Returns current transaction. If there's no transaction, it'll return null.
        /// </summary>
        IDbTransaction CurrentTransaction { get; }

        /// <summary>
        /// To begin a new transaction.
        /// </summary>
        /// <returns></returns>
        IDbTransaction Begin();

        /// <summary>
        /// To begin a new transaction with specific <see cref="IsolationLevel"/>.
        /// </summary>
        /// <param name="li">Special <see cref="IsolationLevel"/></param>
        /// <returns></returns>
        IDbTransaction Begin(IsolationLevel li);

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