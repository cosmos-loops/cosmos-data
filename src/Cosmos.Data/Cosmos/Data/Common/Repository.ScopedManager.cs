using System;
using System.Collections.Concurrent;
using Cosmos.Disposables;

namespace Cosmos.Data.Common
{
    /// <summary>
    /// Scoped repository manager
    /// </summary>
    public sealed class ScopedRepositoryManager : DisposableObjects
    {
        private readonly ConcurrentDictionary<Type, IRepository> _scopedRepositoryInstances;
        private readonly ConcurrentDictionary<Type, RuntimeScopedRepositoryMetadata> _repositoryMetadataDict;

        /// <summary>
        /// Create a new instance of <see cref="ScopedRepositoryManager"/>
        /// </summary>
        public ScopedRepositoryManager()
        {
            _scopedRepositoryInstances = new ConcurrentDictionary<Type, IRepository>();
            _repositoryMetadataDict = new ConcurrentDictionary<Type, RuntimeScopedRepositoryMetadata>();

            AddDisposableAction(Core.Constants.ScopedRepoClearTaskName, () =>
            {
                _scopedRepositoryInstances.Clear();
                _repositoryMetadataDict.Clear();
            });
        }

        /// <summary>
        /// Gets current trance id
        /// </summary>
        public string TranceId { get; private set; }

        private bool _hasSetTraceId;

        internal void Register(Type implementType, RuntimeScopedRepositoryMetadata metadata)
        {
            _scopedRepositoryInstances.TryAdd(implementType, metadata.Instance);
            _repositoryMetadataDict.TryAdd(implementType, metadata);

            _hasSetTraceId.IfFalse(() =>
            {
                TranceId = metadata.TranceId;
                _hasSetTraceId = true;
            });
        }
    }
}