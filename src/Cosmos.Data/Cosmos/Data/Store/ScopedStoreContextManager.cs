using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Cosmos.Data.Store
{
    public sealed class ScopedStoreContextManager : IStoreContextManager
    {
        private readonly ConcurrentDictionary<Type, IStoreContext> _storeContexts;

        public ScopedStoreContextManager()
        {
            _storeContexts = new ConcurrentDictionary<Type, IStoreContext>();
        }

        private IEnumerable<IStoreContext> Stores => _storeContexts.Values;

        public void Commit()
        {
            foreach (var store in Stores)
                store.Commit();
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            foreach (var store in Stores)
                await store.CommitAsync(cancellationToken);
        }

        public void Register(Type type, IStoreContext context)
        {
            context.CheckNull(nameof(context));
            _storeContexts.ContainsKey(type).IfFalse(() => _storeContexts.TryAdd(type, context));
        }
    }
}