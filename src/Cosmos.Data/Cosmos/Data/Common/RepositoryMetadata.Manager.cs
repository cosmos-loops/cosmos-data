using System;
using System.Collections.Concurrent;

namespace Cosmos.Data.Common
{
    /// <summary>
    /// Repository manager
    /// </summary>
    public sealed class RepositoryManager
    {
        #region Static instance

        // ReSharper disable once InconsistentNaming
        private static readonly RepositoryManager _manager = new RepositoryManager();

        /// <summary>
        /// Gets or creates a new instance of <see cref="RepositoryManager"/>
        /// </summary>
        /// <returns></returns>
        public static RepositoryManager Instance => _manager;

        #endregion

        private readonly ConcurrentDictionary<Type, IRepositoryMetadata> _repositoryMetadata;
        private readonly ConcurrentDictionary<Type, Type> _targetTypeMap;

        /// <summary>
        /// Create a new instance of <see cref="RepositoryManager"/>
        /// </summary>
        private RepositoryManager()
        {
            _repositoryMetadata = new ConcurrentDictionary<Type, IRepositoryMetadata>();
            _targetTypeMap = new ConcurrentDictionary<Type, Type>();
        }

        /// <summary>
        /// Register
        /// </summary>
        /// <param name="reflector"></param>
        public void Register(RepositoryReflector reflector)
        {
            switch (reflector.MapType)
            {
                case RepositoryReflector.MappingType.InterfaceToClass:
                    _repositoryMetadata.TryAdd(reflector.ImplementType, reflector.ExportMetadata());
                    _targetTypeMap.TryAdd(reflector.ServiceType, reflector.ImplementType);
                    break;

                case RepositoryReflector.MappingType.AbstractToClass:
                    _repositoryMetadata.TryAdd(reflector.ImplementType, reflector.ExportMetadata());
                    _targetTypeMap.TryAdd(reflector.ServiceType, reflector.ImplementType);
                    break;

                case RepositoryReflector.MappingType.ClassToClass:
                    _repositoryMetadata.TryAdd(reflector.ImplementType, reflector.ExportMetadata());
                    _targetTypeMap.TryAdd(reflector.ServiceType, reflector.ImplementType);
                    break;

                case RepositoryReflector.MappingType.ClassSelf:
                    _repositoryMetadata.TryAdd(reflector.ServiceType, reflector.ExportMetadata());
                    break;

                default:
                    throw new InvalidOperationException("Unknown mapping type.");
            }
        }

        /// <summary>
        /// Require by given type
        /// </summary>
        /// <param name="implementationType"></param>
        /// <returns></returns>
        public IRepositoryMetadata Require(Type implementationType)
        {
            return _targetTypeMap.TryGetValue(implementationType, out var targetType)
                ? _repositoryMetadata.TryGetValue(targetType, out var m0)
                    ? m0
                    : default
                : _repositoryMetadata.TryGetValue(implementationType, out var m1)
                    ? m1
                    : default;
        }

        /// <summary>
        /// Require by typed argument
        /// </summary>
        /// <typeparam name="TImplementation"></typeparam>
        /// <returns></returns>
        public IRepositoryMetadata Require<TImplementation>() where TImplementation : class, IRepository
        {
            return Require(typeof(TImplementation));
        }
    }
}