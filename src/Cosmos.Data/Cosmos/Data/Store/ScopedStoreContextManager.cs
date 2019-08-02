using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Cosmos.Data.Store
{
    /// <summary>
    /// Scoped store context manager
    /// </summary>
    public sealed class ScopedStoreContextManager : IStoreContextManager
    {
        private readonly ConcurrentDictionary<Type, IStoreContext> _storeContexts;

        /// <summary>
        /// Create a new instance of <see cref="ScopedStoreContextManager"/>
        /// </summary>
        public ScopedStoreContextManager()
        {
            _storeContexts = new ConcurrentDictionary<Type, IStoreContext>();
        }

        private IEnumerable<IStoreContext> Stores => _storeContexts.Values;

        /// <summary>
        /// Commit
        /// </summary>
        public void Commit()
        {
            foreach (var store in Stores)
                store.Commit();
        }

        /// <summary>
        /// Commit async
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            foreach (var store in Stores)
                await store.CommitAsync(cancellationToken);
        }

        /// <summary>
        /// Register
        /// </summary>
        /// <param name="type"></param>
        /// <param name="context"></param>
        public void Register(Type type, IStoreContext context)
        {
            context.CheckNull(nameof(context));
            _storeContexts.ContainsKey(type).IfFalse(() => _storeContexts.TryAdd(type, context));
        }
    }
}