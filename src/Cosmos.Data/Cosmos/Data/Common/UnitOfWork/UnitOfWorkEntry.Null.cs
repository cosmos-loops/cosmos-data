using System.Data;
using System.Data.Common;
using Cosmos.Disposables;

namespace Cosmos.Data.Common.UnitOfWork
{
    /// <summary>
    /// Null UnitOfWork Operator
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public sealed class NullUnitOfWorkEntry : DisposableObjects, IUnitOfWorkEntry
    {
        /// <summary>
        /// Gets an instance of <see cref="NullUnitOfWorkEntry"/>
        /// </summary>
        public static NullUnitOfWorkEntry Instance { get; } = new();

        private NullUnitOfWorkEntry() { }

        /// <inheritdoc />
        public DbTransaction GetOrBegin(bool isCreate = true) => null;

        /// <inheritdoc />
        public IsolationLevel? IsolationLevel
        {
            get => null;
            set { }
        }

        /// <summary>
        /// Commit.
        /// </summary>
        public void Commit() { }

        /// <summary>
        /// Rollback
        /// </summary>
        public void Rollback() { }
    }
}