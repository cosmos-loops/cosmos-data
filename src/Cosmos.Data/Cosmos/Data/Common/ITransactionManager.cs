using System;
using System.Data;

namespace Cosmos.Data.Common
{
    /// <summary>
    /// An interface of the append transaction calling wrapper.
    /// </summary>
    public interface ITransactionManager
    {
        /// <summary>
        /// Gets the count of append transaction in this scope.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Register an append transaction into this scope.
        /// </summary>
        /// <param name="transWrapper"></param>
        /// <param name="type"></param>
        /// <param name="isolationLevel"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IUnitOfWorkEntry Register(ITransactionWrapper transWrapper,
            UnitOfWorkTypes type = UnitOfWorkTypes.Required, IsolationLevel? isolationLevel = null);
    }
}