using System.Data;
using Cosmos.Data.Common.UnitOfWork;

namespace Cosmos.Data.Common.Transaction
{
    /// <summary>
    /// An instance of null transaction calling wrapper.
    /// </summary>
    public sealed class NullTransactionManager : ITransactionManager
    {
        /// <summary>
        /// Returns a single instance of <see cref="NullTransactionManager"/>.
        /// </summary>
        public static ITransactionManager Instance { get; } = new NullTransactionManager();

        /// <summary>
        /// Constructor of <see cref="NullTransactionManager"/>.
        /// </summary>
        private NullTransactionManager() { }

        /// <summary>
        /// To flag the sum of func in this wrapper. This instance always returns 0.
        /// </summary>
        public int Count => 0;

        /// <inheritdoc />
        public IUnitOfWorkEntry Register(ITransactionWrapper transWrapper,
            UnitOfWorkTypes type = UnitOfWorkTypes.Required, IsolationLevel? isolationLevel = null)
            => NullUnitOfWorkEntry.Instance;
    }
}