using System;
using System.Data;
using System.Data.Common;
using Cosmos.Data.Common;
using Cosmos.Data.Common.UnitOfWork;

namespace Cosmos.Data.Core.UnitOfWork
{
    /// <summary>
    /// Nothing UoW proxy
    /// </summary>
    public class NothingUowProxy : IUnitOfWorkEntry
    {
        private readonly IUnitOfWorkEntry _baseUow = NullUnitOfWorkEntry.Instance;

        /// <inheritdoc />
        public DbTransaction GetOrBegin(bool isCreate = true) => _baseUow.GetOrBegin(isCreate);

        /// <inheritdoc />
        public IsolationLevel? IsolationLevel { get; set; }

        /// <inheritdoc />
        public void Commit() => _baseUow.Commit();

        /// <inheritdoc />
        public void Rollback() => _baseUow.Rollback();

        internal Action OnDispose;

        /// <inheritdoc />
        public void Dispose() => OnDispose?.Invoke();
    }
}