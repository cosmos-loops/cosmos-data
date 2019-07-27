using System;
using System.Data;
using System.Data.Common;
using Cosmos.Disposables;

namespace Cosmos.Data.Transaction
{
    /// <summary>
    /// A wrapper for transaction.
    /// </summary>
    public class TransactionWrapper : DisposableObjects, ITransactionWrapper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="operator"></param>
        public TransactionWrapper(IDbConnection connection, TransactionAppendOperator @operator = null)
        {
            Connection = connection ?? throw new ArgumentNullException(nameof(connection));
            AppendOperator = @operator;
            AddDisposableAction("_transactionWrapper", () => CurrentTransaction?.Dispose());
        }

        public TransactionWrapper(IDbConnection connection, IsolationLevel level, TransactionAppendOperator @operator = null)
        {
            Connection = connection ?? throw new ArgumentNullException(nameof(connection));
            IsolationLevel = level;
            AppendOperator = @operator;
            AddDisposableAction("_transactionWrapper", () => CurrentTransaction?.Dispose());
        }

        private TransactionWrapper(IDbConnection connection, IDbTransaction transaction, IsolationLevel level, TransactionAppendOperator @operator = null)
        {
            Connection = connection ?? throw new ArgumentNullException(nameof(connection));
            IsolationLevel = level;
            CurrentTransaction = transaction;
            AppendOperator = @operator;
            AddDisposableAction("_transactionWrapper", () => CurrentTransaction?.Dispose());
        }

        public TransactionWrapper(TransactionWrapper wrapper)
        {
            wrapper.CheckNull(nameof(wrapper));
            Connection = wrapper.Connection;
            IsolationLevel = wrapper.IsolationLevel;
            CurrentTransaction = wrapper.CurrentTransaction;
            AppendOperator = wrapper.AppendOperator;
            AddDisposableAction("_transactionWrapper", () => CurrentTransaction?.Dispose());
        }

        private IDbConnection Connection { get; }

        /// <summary>
        /// Returns current transaction. If there's no transaction, it'll return null.
        /// </summary>
        public IDbTransaction CurrentTransaction { get; private set; }

        /// <summary>
        /// Gets the <see cref="IsolationLevel"/> of current transaction.
        /// </summary>
        public IsolationLevel IsolationLevel { get; } = IsolationLevel.ReadCommitted;

        private TransactionAppendOperator AppendOperator { get; }

        private void OpenConnectionIfNeed()
        {
            if (Connection.State == ConnectionState.Closed)
                Connection.Open();
        }

        /// <summary>
        /// To begin a new transaction.
        /// </summary>
        /// <returns></returns>
        public IDbTransaction Begin()
        {
            if (CurrentTransaction == null)
            {
                OpenConnectionIfNeed();
                CurrentTransaction = Connection.BeginTransaction(IsolationLevel);
            }

            return CurrentTransaction;
        }

        /// <summary>
        /// To begin a new transaction with specific <see cref="IsolationLevel"/>.
        /// </summary>
        /// <param name="li">Special <see cref="IsolationLevel"/></param>
        /// <returns></returns>
        public IDbTransaction Begin(IsolationLevel il)
        {
            if (CurrentTransaction == null)
            {
                OpenConnectionIfNeed();
                CurrentTransaction = Connection.BeginTransaction(il);
            }

            return CurrentTransaction;
        }

        /// <summary>
        /// Commit.
        /// </summary>
        /// <param name="clearTransaction">Do need clear transaction after commit.</param>
        public void Commit(bool clearTransaction = true)
        {
            AppendOperator?.BeforeCommit?.Invoke();
            CurrentTransaction.Commit();
            AppendOperator?.AfterCommit?.Invoke();

            if (clearTransaction)
                CurrentTransaction = null;
        }

        /// <summary>
        /// Rollback.
        /// </summary>
        /// <param name="clearTransaction">Do need clear transaction after rollback.</param>
        public void Rollback(bool clearTransaction = true)
        {
            AppendOperator?.BeforeRollback?.Invoke();
            CurrentTransaction.Rollback();
            AppendOperator?.AfterRollback?.Invoke();

            if (clearTransaction)
                CurrentTransaction = null;
        }

        /// <summary>
        /// To flag this instance is NullTransactionWrapper or not. This will always return false.
        /// </summary>
        public bool IsNull => false;

        /// <summary>
        /// To create a new instance of <see cref="TransactionWrapper"/>.
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="operator"></param>
        /// <returns></returns>
        public static ITransactionWrapper CreateFromTransaction(IDbTransaction transaction, TransactionAppendOperator @operator = null)
        {
            transaction.CheckNull(nameof(transaction));
            var connection = transaction.Connection;
            var level = transaction.IsolationLevel;
            return new TransactionWrapper(connection, transaction, level, @operator);
        }

        /// <summary>
        /// Returns a new instance of <see cref="NullTransactionWrapper"/>.
        /// </summary>
        /// <returns></returns>
        public static ITransactionWrapper CreateNull()
        {
            return new NullTransactionWrapper();
        }


    }
}