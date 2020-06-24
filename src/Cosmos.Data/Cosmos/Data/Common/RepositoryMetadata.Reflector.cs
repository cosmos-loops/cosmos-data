using System;
using Cosmos.Reflection;

namespace Cosmos.Data.Common
{
    /// <summary>
    /// Runtime metadata for repository
    /// </summary>
    public class RepositoryReflector: IRepositoryMetadata
    {
        private RepositoryReflector() { }

        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        public Type ServiceType { get; set; }

        /// <inheritdoc />
        public Type ImplementType { get; set; }

        /// <summary>
        /// Mapping type
        /// </summary>
        public MappingType MapType { get; set; }

        /// <summary>
        /// Export static metadata
        /// </summary>
        /// <returns></returns>
        public IRepositoryMetadata ExportMetadata()
        {
            return new RepositoryMetadata
            {
                Name = Name,
                ServiceType = ServiceType,
                ImplementType = ImplementType
            };
        }

        /// <summary>
        /// Metadata mapping types
        /// </summary>
        public enum MappingType
        {
            /// <summary>
            /// Interface to class
            /// </summary>
            InterfaceToClass,

            /// <summary>
            /// Abstract class to class
            /// </summary>
            AbstractToClass,

            /// <summary>
            /// Base class to class
            /// </summary>
            ClassToClass,

            /// <summary>
            /// Class-self
            /// </summary>
            ClassSelf
        }

        /// <summary>
        /// Create for repository, and return an instance of runtime repository metadata object.
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplement"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static RepositoryReflector Create<TService, TImplement>()
            where TService : IRepository
            where TImplement : class, TService
        {
            var typeOfService = typeof(TService);
            var typeOfImplement = typeof(TImplement);

            if (typeOfImplement.IsInterface || typeOfImplement.IsAbstract)
            {
                throw new ArgumentException($"The implement type {typeOfImplement} cannot be created an instance.");
            }

            if (typeOfService.IsInterface)
            {
                var attribute = typeOfService.GetAttribute<RepositoryAttribute>();
                return CreateCore(typeOfService, typeOfImplement, MappingType.InterfaceToClass, GetName(typeOfImplement, attribute));
            }

            if (typeOfService.IsClass && typeOfService.IsAbstract)
            {
                var attribute = typeOfService.GetAttribute<RepositoryAttribute>();
                return CreateCore(typeOfService, typeOfImplement, MappingType.AbstractToClass, GetName(typeOfImplement, attribute));
            }

            if (typeOfService.IsClass)
            {
                var attribute = typeOfService.GetAttribute<RepositoryAttribute>();
                return CreateCore(typeOfService, typeOfImplement, MappingType.ClassToClass, GetName(typeOfImplement, attribute));
            }

            throw new InvalidOperationException("Unknown service type.");
        }

        /// <summary>
        /// Create for repository, and return an instance of runtime repository metadata object.
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static RepositoryReflector Create<TService>()
        {
            var typeOfService = typeof(TService);

            if (typeOfService.IsInterface)
            {
                var attribute = typeOfService.GetAttribute<RepositoryAttribute>();

                if (attribute is null)
                    throw new InvalidOperationException("Cannot find the target type for this interface.");

                var typeOfImplement = attribute.MapTo;

                if (typeOfImplement is null)
                    throw new InvalidOperationException("The target type for this interface cannot be null.");

                if (typeOfImplement.IsInterface || typeOfImplement.IsAbstract)
                    throw new ArgumentException($"The implement type {typeOfImplement} cannot be created an instance.");

                return CreateCore(typeOfService, typeOfImplement, MappingType.InterfaceToClass, GetName(typeOfImplement, attribute));
            }

            if (typeOfService.IsAbstract)
            {
                throw new ArgumentException($"The abstract service type {typeOfService} cannot be created an instance.");
            }

            if (typeOfService.IsClass)
            {
                var attribute = typeOfService.GetAttribute<RepositoryAttribute>();
                return CreateCore(typeOfService, typeOfService, MappingType.ClassSelf, GetName(typeOfService, attribute));
            }

            throw new InvalidOperationException("Unknown service type.");
        }

        private static RepositoryReflector CreateCore(Type serviceType, Type implementType, MappingType type, string name)
        {
            return new RepositoryReflector
            {
                Name = name,
                ServiceType = serviceType,
                ImplementType = implementType,
                MapType = type
            };
        }

        private static string GetName(Type typeOfImplement, RepositoryAttribute attribute)
        {
            if (attribute is null)
                return typeOfImplement?.Name;
            if (!string.IsNullOrWhiteSpace(attribute.Name))
                return attribute.Name;
            return typeOfImplement?.Name;
        }
    }
}