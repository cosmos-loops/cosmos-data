using System.Data;
using Cosmos.Disposables;

namespace Cosmos.Data.Transaction
{
    /// <summary>
    /// A wrapper for null transaction.
    /// </summary>
    public sealed class NullTransactionWrapper : DisposableObjects, ITransactionWrapper
    {
        /// <summary>
        /// Returns current transaction. If there's no transaction, it'll return null.
        /// </summary>
        public IDbTransaction CurrentTransaction => NullDbTransaction.Instance;

        /// <summary>
        /// To begin a new transaction.
        /// </summary>
        /// <returns></returns>
        public IDbTransaction Begin() => NullDbTransaction.Instance;

        /// <summary>
        /// To begin a new transaction with specific <see cref="IsolationLevel"/>.
        /// </summary>
        /// <param name="li">Special <see cref="IsolationLevel"/></param>
        /// <returns></returns>
        public IDbTransaction Begin(IsolationLevel li) => NullDbTransaction.Instance;

        /// <summary>
        /// Commit.
        /// </summary>
        /// <param name="clearTransaction">Do need clear transaction after commit.</param>
        public void Commit(bool clearTransaction = true) { }

        /// <summary>
        /// Rollback.
        /// </summary>
        /// <param name="clearTransaction">Do need clear transaction after rollback.</param>
        public void Rollback(bool clearTransaction = true) { }

        /// <summary>
        /// To flag this instance is NullTransactionWrapper or not. This will always return true.
        /// </summary>
        public bool IsNull => true;
    }
}