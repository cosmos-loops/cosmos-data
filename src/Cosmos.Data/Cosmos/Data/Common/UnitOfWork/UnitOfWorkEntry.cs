using System;
using System.Data;
using System.Data.Common;
using Cosmos.Disposables;

namespace Cosmos.Data.Common.UnitOfWork
{
    /// <summary>
    /// UnitOfWork operator, an entry of <see cref="ITransactionWrapper"/>
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public class UnitOfWorkEntry : DisposableObjects, IUnitOfWorkEntry
    {
        /// <summary>
        /// Create a new instance of <see cref="UnitOfWorkEntry"/>
        /// </summary>
        /// <param name="transWrapper"></param>
        /// <param name="manualCommit"></param>
        public UnitOfWorkEntry(ITransactionWrapper transWrapper, bool manualCommit = false)
        {
            transWrapper.CheckNull(nameof(transWrapper));
            InternalTransaction = transWrapper;
            AddDisposableAction("_unitOfWorkDisposeAction", DefaultDisposeAction);
            Committed = false;
            ManualCommit = manualCommit;
        }

        /// <summary>
        /// Create a new instance of <see cref="UnitOfWorkEntry"/>
        /// </summary>
        /// <param name="transWrapper"></param>
        /// <param name="disposeAction"></param>
        /// <param name="manualCommit"></param>
        public UnitOfWorkEntry(ITransactionWrapper transWrapper, Action disposeAction, bool manualCommit = false)
        {
            transWrapper.CheckNull(nameof(transWrapper));
            InternalTransaction = transWrapper;
            AddDisposableAction("_unitOfWorkDisposeAction", disposeAction ?? DefaultDisposeAction);
            Committed = false;
            ManualCommit = manualCommit;
        }

        /// <summary>
        /// Gets UnitOfWork Manager
        /// </summary>
        protected ITransactionWrapper InternalTransaction { get; }

        private void DefaultDisposeAction()
        {
            CommitWhenDisposing();
            Committed = true;
        }

        private bool Committed { get; set; }

        private bool ManualCommit { get; set; }

        private bool NeedToCommitWhenDispose => !Committed && !ManualCommit;

        /// <inheritdoc />
        public DbTransaction GetOrBegin(bool isCreate = true) => InternalTransaction.GetOrBegin(isCreate);

        /// <inheritdoc />
        public IsolationLevel? IsolationLevel
        {
            get => InternalTransaction.IsolationLevel;
            set => InternalTransaction.IsolationLevel = value;
        }

        /// <summary>
        /// Commit
        /// </summary>
        public void Commit()
        {
            if (!Committed)
            {
                InternalTransaction.Commit();
                Committed = true;
            }
        }

        /// <summary>
        /// Rollback
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Rollback()
        {
            InternalTransaction.Rollback();
        }

        private void CommitWhenDisposing()
        {
            if (NeedToCommitWhenDispose)
            {
                try
                {
                    Commit();
                }
                catch
                {
                    Rollback();
                }
            }
        }
    }
}