using System;
using System.Collections.Generic;
using System.Data;
using Cosmos.Data.Core.UnitOfWork;

namespace Cosmos.Data.Common.Transaction
{
    /// <summary>
    /// A scoped type transaction calling wrapper.
    /// </summary>
    public sealed class ScopedTransactionManager : ITransactionManager
    {
        private readonly List<UowRef> _uowRefs = new List<UowRef>();
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        /// <summary>
        /// Constructor of <see cref="ScopedTransactionManager"/>.
        /// </summary>
        public ScopedTransactionManager(IUnitOfWorkManager unitOfWorkManager)
        {
            _unitOfWorkManager = unitOfWorkManager ?? UnitOfWorkManagerHolder.Instance;
        }

        /// <inheritdoc />
        public int Count => _uowRefs.Count;

        /// <inheritdoc />
        public IUnitOfWorkEntry Register(ITransactionWrapper transWrapper,
            UnitOfWorkTypes type = UnitOfWorkTypes.Required, IsolationLevel? isolationLevel = null)
        {
            if (transWrapper is null)
                throw new ArgumentNullException(nameof(transWrapper));
            var entry = _unitOfWorkManager.CreateUnitOfWork(transWrapper, type, isolationLevel);
            _uowRefs.Add(UowRefNew.Virtual(entry, false, @ref => _uowRefs.Remove(@ref)));
            return entry;
        }
    }
}