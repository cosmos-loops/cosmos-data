using System;
using System.Data;
using System.Data.Common;
using Cosmos.Data.Common;

namespace Cosmos.Data.Core.UnitOfWork
{
    /// <summary>
    /// Virtual UoW proxy
    /// </summary>
    public class VirtualUowProxy : IUnitOfWorkEntry
    {
        private readonly IUnitOfWorkEntry _baseUow;

        /// <summary>
        /// Create a new instance of <see cref="VirtualUowProxy"/>
        /// </summary>
        /// <param name="entry"></param>
        public VirtualUowProxy(IUnitOfWorkEntry entry)
        {
            _baseUow = entry;
        }

        /// <inheritdoc />
        public DbTransaction GetOrBegin(bool isCreate = true) => _baseUow.GetOrBegin(isCreate);

        /// <inheritdoc />
        public IsolationLevel? IsolationLevel
        {
            get => _baseUow.IsolationLevel;
            set { }
        }

        /// <inheritdoc />
        public void Commit() { }

        /// <inheritdoc />
        public void Rollback() => _baseUow.Rollback();

        internal Action OnDispose;

        /// <inheritdoc />
        public void Dispose() => OnDispose?.Invoke();
    }
}