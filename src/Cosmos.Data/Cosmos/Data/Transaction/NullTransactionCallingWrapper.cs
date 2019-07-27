using System;
using System.Data;
using System.Threading.Tasks;

namespace Cosmos.Data.Transaction
{
    /// <summary>
    /// An instance of null transaction calling wrapper.
    /// </summary>
    public sealed class NullTransactionCallingWrapper : ITransactionCallingWrapper
    {
        /// <summary>
        /// Returns a single instance of <see cref="NullTransactionCallingWrapper"/>.
        /// </summary>
        public static ITransactionCallingWrapper Instance { get; } = new NullTransactionCallingWrapper();

        /// <summary>
        /// Constructor of <see cref="NullTransactionCallingWrapper"/>.
        /// </summary>
        private NullTransactionCallingWrapper() { }

        /// <summary>
        /// To flag the sum of func in this wrapper. This instance always returns 0.
        /// </summary>
        public int Count => 0;

        /// <summary>
        /// Commit async. The method in this instance will do nothing.
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public Task CommitAsync(IDbTransaction transaction) => Task.CompletedTask;

        /// <summary>
        /// Register a transaction calling into such wrapper. The method in this instance will do nothing. 
        /// </summary>
        /// <param name="func"></param>
        public void Register(Func<IDbTransaction, Task> func) { }
    }
}
