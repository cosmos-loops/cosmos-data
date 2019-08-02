using System;
using System.Collections.Concurrent;
using Cosmos.Disposables;
using Cosmos.IdUtils;

namespace Cosmos.Data.Store
{
    /// <summary>
    /// Scoped repository manager
    /// </summary>
    public sealed class ScopedRepositoryManager : DisposableObjects
    {
        private readonly ConcurrentDictionary<Type, object> _scopedRepositoryInstances;
        private readonly ConcurrentDictionary<Type, ScopedRepositoryMetadata> _repositoryMetadatas;

        /// <summary>
        /// Create a new instance of <see cref="ScopedRepositoryManager"/>
        /// </summary>
        public ScopedRepositoryManager()
        {
            _scopedRepositoryInstances = new ConcurrentDictionary<Type, object>();
            _repositoryMetadatas = new ConcurrentDictionary<Type, ScopedRepositoryMetadata>();

            AddDisposableAction("_instanceClear", () =>
            {
                _scopedRepositoryInstances.Clear();
                _repositoryMetadatas.Clear();
            });
        }

        internal void Register(Type implementType, object instance, RepositoryMetadata metadata, TraceIdAccessor accessor)
        {
            _scopedRepositoryInstances.TryAdd(implementType, instance);
            _repositoryMetadatas.TryAdd(implementType, new ScopedRepositoryMetadata(metadata));
        }
    }
}