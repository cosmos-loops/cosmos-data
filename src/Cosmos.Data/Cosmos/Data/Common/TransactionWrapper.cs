using System;
using System.Collections.Concurrent;
using System.Data;
using System.Data.Common;
using System.Threading;
using Cosmos.Data.Core.Transaction;
using Cosmos.Disposables;

namespace Cosmos.Data.Common
{
    /// <summary>
    /// A wrapper for transaction.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public class TransactionWrapper : DisposableObjects, ITransactionWrapper, IUnitOfWorkOperator
    {
        /// <summary>
        /// Seed number <br />
        /// 种子数
        /// </summary>
        private static int _seed;

        private BeforeTransNotifyArgs _beforeArgs;
        private BeforeTransNotifyArgs _beforeArgsW;

        /// <summary>
        /// Watching window for Transaction wrapper <br />
        /// 用于调试的观察口
        /// </summary>
        public static ConcurrentDictionary<string, ITransactionWrapper> WatchingWindow { get; } = new ConcurrentDictionary<string, ITransactionWrapper>();

        /// <summary>
        /// Create a new instance of <see cref="TransactionWrapper"/>
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="operator"></param>
        public TransactionWrapper(DbConnection connection, TransactionAppendOperator @operator = null)
        {
            Connection = connection ?? throw new ArgumentNullException(nameof(connection));
            AppendOperator = @operator;
            AddDisposableAction(Core.Constants.TransWpClearTaskName, DisposableCallback);
            _beforeArgsW = new BeforeTransNotifyArgs(string.Empty, "The TransactionWrapper instance is created.");
            AppendOperator?.BeforeTransNotify?.Invoke(this, _beforeArgsW);
        }

        /// <summary>
        /// Create a new instance of <see cref="TransactionWrapper"/>
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="level"></param>
        /// <param name="operator"></param>
        public TransactionWrapper(DbConnection connection, IsolationLevel level, TransactionAppendOperator @operator = null)
        {
            Connection = connection ?? throw new ArgumentNullException(nameof(connection));
            IsolationLevel = level;
            AppendOperator = @operator;
            AddDisposableAction(Core.Constants.TransWpClearTaskName, DisposableCallback);
            _beforeArgsW = new BeforeTransNotifyArgs(string.Empty, "The TransactionWrapper instance is created.");
            AppendOperator?.BeforeTransNotify?.Invoke(this, _beforeArgsW);
        }

        private TransactionWrapper(DbConnection connection, DbTransaction transaction, IsolationLevel? level, TransactionAppendOperator @operator = null)
        {
            Connection = connection ?? throw new ArgumentNullException(nameof(connection));
            IsolationLevel = level;
            CurrentTransaction = transaction;
            AppendOperator = @operator;
            AddDisposableAction(Core.Constants.TransWpClearTaskName, DisposableCallback);
            _beforeArgsW = new BeforeTransNotifyArgs(string.Empty, "The TransactionWrapper instance is created.");
            AppendOperator?.BeforeTransNotify?.Invoke(this, _beforeArgsW);
        }

        /// <summary>
        /// Create a new instance of <see cref="TransactionWrapper"/>
        /// </summary>
        /// <param name="wrapper"></param>
        public TransactionWrapper(TransactionWrapper wrapper)
        {
            wrapper.CheckNull(nameof(wrapper));
            Connection = wrapper.Connection;
            IsolationLevel = wrapper.IsolationLevel;
            CurrentTransaction = wrapper.CurrentTransaction;
            AppendOperator = wrapper.AppendOperator;
            AddDisposableAction(Core.Constants.TransWpClearTaskName, DisposableCallback);
            _beforeArgsW = new BeforeTransNotifyArgs(string.Empty, "The TransactionWrapper instance is created.");
            AppendOperator?.BeforeTransNotify?.Invoke(this, _beforeArgsW);
        }

        #region Connection and current transaction

        /// <summary>
        /// Return current connection.<br />
        /// 获得当前连接。<br />
        /// 如果使用连接池，则请从资源池中直接获得
        /// </summary>
        private DbConnection Connection { get; set; }

        /// <summary>
        /// Returns current transaction. If there's no transaction, it'll return null.
        /// </summary>
        private DbTransaction CurrentTransaction { get; set; }

        #endregion

        #region Transaction Id

        /// <inheritdoc />
        public string Id { get; private set; }

        #endregion

        #region Get or begin transaction

        /// <inheritdoc />
        public DbTransaction GetOrBegin(bool isCreate = true)
        {
            if (CurrentTransaction is not null)
                return CurrentTransaction;

            if (isCreate is false)
                return NullDbTransaction.Instance;

            try
            {
                OpenConnectionIfNeed();

                try
                {
                    CurrentTransaction = IsolationLevel is null
                        ? Connection.BeginTransaction()
                        : Connection.BeginTransaction(IsolationLevel.Value);

                    Id = $"{DateTime.Now:yyyyMMdd_HHmmss}_{Interlocked.Increment(ref _seed)}";

                    WatchingWindow.TryAdd(Id, this);

                    if (AppendOperator != null)
                    {
                        _beforeArgs = new BeforeTransNotifyArgs(Id, Resource.BeginTransactionSuccess) {IsolationLevel = IsolationLevel};
                        AppendOperator.BeforeTransNotify?.Invoke(this, _beforeArgs);
                    }
                }
                catch
                {
                    ReturnObjectIfNeed();
                    throw;
                }
            }
            catch (Exception exception)
            {
                AppendOperator?.AfterTransNotify?.Invoke(this, new AfterTransNotifyArgs(_beforeArgs, Resource.BeginTransactionFailed, exception));
                throw;
            }

            return CurrentTransaction;
        }

        /// <summary>
        /// Gets the <see cref="IsolationLevel"/> of current transaction.
        /// </summary>
        public IsolationLevel? IsolationLevel { get; set; }

        /// <summary>
        /// Append transaction operator<br />
        /// 额外的事务操作
        /// </summary>
        private TransactionAppendOperator AppendOperator { get; }

        #endregion

        #region Prepare or return resource

        /// <summary>
        /// Open connection if need<br />
        /// 如需要，开启连接。<br />
        /// 如果连接资源由对象池托管，则尝试从资源池中获取连接对象
        /// </summary>
        protected virtual void OpenConnectionIfNeed()
        {
            // TODO
            // if (Connection == null)
            // {
            //     Get conn object from pool
            // }

            if (Connection.State == ConnectionState.Closed)
                Connection.Open();
        }

        /// <summary>
        /// Return object if need<br />
        /// 如需要，关闭并归还连接<br />
        /// 如果连接由对象池托管，则尝试向资源池归还该连接对象
        /// </summary>
        protected virtual void ReturnObjectIfNeed()
        {
            if (!string.IsNullOrWhiteSpace(Id) && WatchingWindow.TryRemove(Id, out _))
                Id = null;

            //Return object
            CurrentTransaction = null;
            Connection = null;
        }

        #endregion

        #region Commit or Rollback

        /// <summary>
        /// Commit.
        /// </summary>
        /// <param name="clearTransaction">Do need clear transaction after commit.</param>
        public virtual void Commit(bool clearTransaction = true)
        {
            var isFinishToCommit = false;

            try
            {
                if (CurrentTransaction != null)
                {
                    AppendOperator?.BeforeCommit?.Invoke();

                    CurrentTransaction.Commit();

                    isFinishToCommit = true;
                    AppendOperator?.AfterTransNotify?.Invoke(this, new AfterTransNotifyArgs(_beforeArgs, Resource.CommitFinished));

                    AppendOperator?.AfterCommit?.Invoke();

                    if (clearTransaction)
                        CurrentTransaction = null;
                }
            }
            catch (Exception exception)
            {
                if (isFinishToCommit == false)
                {
                    AppendOperator?.AfterTransNotify?.Invoke(this, new AfterTransNotifyArgs(_beforeArgs, Resource.CommitFailed, exception));
                }

                throw;
            }
            finally
            {
                ReturnObjectIfNeed();
                _beforeArgs = null;
            }
        }

        /// <summary>
        /// Rollback.
        /// </summary>
        /// <param name="clearTransaction">Do need clear transaction after rollback.</param>
        public void Rollback(bool clearTransaction = true)
        {
            var isFinishedRollback = false;

            try
            {
                if (CurrentTransaction != null)
                {
                    AppendOperator?.BeforeRollback?.Invoke();

                    CurrentTransaction.Rollback();

                    isFinishedRollback = true;
                    AppendOperator?.AfterTransNotify?.Invoke(this, new AfterTransNotifyArgs(_beforeArgs, Resource.RollbackFinished));

                    AppendOperator?.AfterRollback?.Invoke();

                    if (clearTransaction)
                        CurrentTransaction = null;
                }
            }
            catch (Exception exception)
            {
                if (!isFinishedRollback)
                {
                    AppendOperator?.AfterTransNotify?.Invoke(this, new AfterTransNotifyArgs(_beforeArgs, Resource.RollbackFailed, exception));
                }

                throw;
            }
            finally
            {
                ReturnObjectIfNeed();
                _beforeArgs = null;
            }
        }

        #endregion

        #region Null flag

        /// <summary>
        /// To flag this instance is NullTransactionWrapper or not. This will always return false.
        /// </summary>
        public bool IsNull => false;

        #endregion

        #region Dispose

        /// <summary>
        /// Dispose counter
        /// </summary>
        int _disposeCounter;

        /// <summary>
        /// Disposable acton
        /// </summary>
        protected virtual void DisposableCallback()
        {
            CurrentTransaction?.Dispose();

            if (Interlocked.Increment(ref _disposeCounter) != 1) return;

            try
            {
                Rollback();
            }
            finally
            {
                AppendOperator?.AfterTransNotify?.Invoke(this, new AfterTransNotifyArgs(_beforeArgsW, Resource.InstanceReleased));
                GC.SuppressFinalize(this);
            }
        }

        /// <summary>
        /// Dispose
        /// </summary>
        ~TransactionWrapper() => Dispose();

        #endregion

        #region Create

        /// <summary>
        /// To create a new instance of <see cref="TransactionWrapper"/>.
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="operator"></param>
        /// <returns></returns>
        public static ITransactionWrapper CreateFromTransaction(DbTransaction transaction, TransactionAppendOperator @operator = null)
        {
            transaction.CheckNull(nameof(transaction));
            var connection = transaction.Connection;
            var level = transaction.IsolationLevel;
            return new TransactionWrapper(connection, transaction, level, @operator);
        }

        internal static TransactionWrapper CreateFromWrapper(ITransactionWrapper wrapper, TransactionAppendOperator @operator = null)
        {
            wrapper.CheckNull(nameof(wrapper));
            var connection = wrapper.GetOrBegin(false).Connection;
            var transaction = wrapper.GetOrBegin(false);
            var level = wrapper.IsolationLevel;
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

        #endregion

        private static class Resource
        {
            public const string BeginTransactionSuccess = "Begin Transaction";
            public const string BeginTransactionFailed = "The transaction failed to begin or get.";
            public const string CommitFinished = "Submitted";
            public const string CommitFailed = "Submission Failed.";
            public const string RollbackFinished = "Rollback";
            public const string RollbackFailed = "Rollback failed.";
            public const string InstanceReleased = "The TransactionWrapper instance is released.";
        }
    }
}