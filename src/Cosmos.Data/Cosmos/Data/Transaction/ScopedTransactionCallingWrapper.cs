using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Cosmos.Data.Transaction
{
    /// <summary>
    /// A scoped type transaction calling wrapper.
    /// </summary>
    public sealed class ScopedTransactionCallingWrapper : ITransactionCallingWrapper
    {
        private readonly List<Func<IDbTransaction, Task>> _funcs;

        /// <summary>
        /// Constructor of <see cref="ScopedTransactionCallingWrapper"/>.
        /// </summary>
        public ScopedTransactionCallingWrapper()
        {
            _funcs = new List<Func<IDbTransaction, Task>>();
        }

        /// <summary>
        /// To flag the sum of func in this wrapper.
        /// </summary>
        public int Count => _funcs.Count;

        /// <summary>
        /// Register a transaction calling into such wrapper.
        /// </summary>
        /// <param name="func"></param>
        public void Register(Func<IDbTransaction, Task> func)
        {
            if (func == null)
                return;

            _funcs.Add(func);
        }

        /// <summary>
        /// Commit async.
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public async Task CommitAsync(IDbTransaction transaction)
        {
            foreach (var func in _funcs)
                await func(transaction);
            _funcs.Clear();
        }
    }
}