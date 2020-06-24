using System;
using System.Data;
using System.Data.Common;
using Cosmos.Data.Common;

namespace Cosmos.Data.Core.UnitOfWork
{
    /// <summary>
    /// Original UoW proxy
    /// </summary>
    public class OriginalUowProxy : IUnitOfWorkEntry
    {
        private readonly IUnitOfWorkEntry _baseUow;

        /// <summary>
        /// Create a new instance of <see cref="OriginalUowProxy"/>
        /// </summary>
        /// <param name="entry"></param>
        public OriginalUowProxy(IUnitOfWorkEntry entry)
        {
            _baseUow = entry;
        }

        /// <inheritdoc />
        public IsolationLevel? IsolationLevel
        {
            get => _baseUow.IsolationLevel;
            set => _baseUow.IsolationLevel = value;
        }

        /// <inheritdoc />
        public DbTransaction GetOrBegin(bool isCreate = true) => _baseUow.GetOrBegin(isCreate);

        /// <inheritdoc />
        public void Commit() => _baseUow.Commit();

        /// <inheritdoc />
        public void Rollback() => _baseUow.Rollback();

        internal Action OnDispose;

        /// <inheritdoc />
        public void Dispose()
        {
            _baseUow.Dispose();
            OnDispose?.Invoke();
        }
    }
}