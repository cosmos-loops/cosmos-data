using System;
using System.Data;
using Cosmos.Disposables;
using FreeSql;

namespace Cosmos.Data.Transaction
{
    public class FreeSqlTransactionWrapper : DisposableObjects, ITransactionWrapper
    {
        public FreeSqlTransactionWrapper(IUnitOfWork uow, TransactionAppendOperator @operator = null)
        {
            _unitOfWork = uow ?? throw new ArgumentNullException(nameof(uow));
            IsolationLevel = IsolationLevel.ReadCommitted;
            AppendOperator = @operator;
            AddDisposableAction("_transactionWrapper", () => CurrentTransaction?.Dispose());
        }

        public FreeSqlTransactionWrapper(IUnitOfWork uow, IsolationLevel level, TransactionAppendOperator @operator = null)
        {
            _unitOfWork = uow ?? throw new ArgumentNullException(nameof(uow));
            IsolationLevel = level;
            AppendOperator = @operator;
            AddDisposableAction("_transactionWrapper", () => CurrentTransaction?.Dispose());
        }

        private FreeSqlTransactionWrapper(IUnitOfWork uow, IDbTransaction transaction, IsolationLevel level, TransactionAppendOperator @operator = null)
        {
            _unitOfWork = uow ?? throw new ArgumentNullException(nameof(uow));
            IsolationLevel = level;
            CurrentTransaction = transaction;
            AppendOperator = @operator;
            AddDisposableAction("_transactionWrapper", () => CurrentTransaction?.Dispose());
        }

        public FreeSqlTransactionWrapper(FreeSqlTransactionWrapper wrapper)
        {
            wrapper.CheckNull(nameof(wrapper));
            _unitOfWork = wrapper._unitOfWork;
            IsolationLevel = wrapper.IsolationLevel;
            CurrentTransaction = wrapper.CurrentTransaction;
            AppendOperator = wrapper.AppendOperator;
            AddDisposableAction("_transactionWrapper", () => CurrentTransaction?.Dispose());
        }

        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Returns current transaction. If there's no transaction, it'll return null.
        /// </summary>
        public IDbTransaction CurrentTransaction { get; private set; }

        private IsolationLevel _isolationLevel = IsolationLevel.ReadCommitted;

        /// <summary>
        /// Gets the <see cref="IsolationLevel"/> of current transaction.
        /// </summary>
        public IsolationLevel IsolationLevel
        {
            get => _isolationLevel;
            private set => _unitOfWork.IsolationLevel = _isolationLevel = value;
        }

        private TransactionAppendOperator AppendOperator { get; }

        public IDbTransaction Begin() => CurrentTransaction ?? (CurrentTransaction = _unitOfWork.GetOrBeginTransaction());

        public IDbTransaction Begin(IsolationLevel li)
        {
            if (CurrentTransaction != null)
            {
                IsolationLevel = li;
                CurrentTransaction = _unitOfWork.GetOrBeginTransaction();
            }

            return CurrentTransaction;
        }

        public void Commit(bool clearTransaction = true)
        {
            AppendOperator?.BeforeCommit?.Invoke();
            CurrentTransaction.Commit();
            AppendOperator?.AfterCommit?.Invoke();

            if (clearTransaction)
                CurrentTransaction = null;
        }

        public void Rollback(bool clearTransaction = true)
        {
            _unitOfWork.Rollback();
        }

        public bool IsNull => false;


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