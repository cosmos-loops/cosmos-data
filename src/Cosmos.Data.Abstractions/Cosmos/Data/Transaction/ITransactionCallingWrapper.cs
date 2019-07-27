using System;
using System.Data;
using System.Threading.Tasks;

namespace Cosmos.Data.Transaction
{
    /// <summary>
    /// An interface of the transaction calling wrapper.
    /// </summary>
    public interface ITransactionCallingWrapper
    {
        /// <summary>
        /// To flag the sum of func in this wrapper.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Register a transaction calling into such wrapper.
        /// </summary>
        /// <param name="func"></param>
        void Register(Func<IDbTransaction, Task> func);

        /// <summary>
        /// Commit async.
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        Task CommitAsync(IDbTransaction transaction);
    }
}