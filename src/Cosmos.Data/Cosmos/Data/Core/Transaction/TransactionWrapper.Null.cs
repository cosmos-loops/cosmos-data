using System.Data;
using System.Data.Common;
using Cosmos.Data.Common;
using Cosmos.Disposables;

namespace Cosmos.Data.Core.Transaction
{
    /// <summary>
    /// A wrapper for null transaction.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public sealed class NullTransactionWrapper : DisposableObjects, ITransactionWrapper, IUnitOfWorkOperator
    {
        /// <inheritdoc />
        public string Id { get; } = string.Empty;

        /// <inheritdoc />
        public DbTransaction GetOrBegin(bool isCreate = true) => NullDbTransaction.Instance;

        /// <inheritdoc />
        public IsolationLevel? IsolationLevel { get; set; }

        /// <inheritdoc />
        public void Commit(bool clearTransaction = true) { }

        /// <inheritdoc />
        public void Rollback(bool clearTransaction = true) { }

        /// <inheritdoc />
        public bool IsNull => true;
    }
}