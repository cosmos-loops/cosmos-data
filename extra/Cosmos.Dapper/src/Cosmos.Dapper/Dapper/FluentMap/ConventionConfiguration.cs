using System;
using System.Linq;
using System.Reflection;
using Cosmos.Dapper.Conventions;
using Cosmos.Dapper.Core.Mapping;
using Cosmos.Dapper.Core.Mapping.Filters;
using Dapper.Mapper;

namespace Dapper.FluentMap
{
    public class FluentConventionConfiguration
    {
        private readonly ConventionBase _convention;
        private readonly IDapperMappingConfig _mappingConfig;

        public FluentConventionConfiguration(ConventionBase convention, IDapperMappingConfig mappingConfig)
        {
            _convention = convention;
            _mappingConfig = mappingConfig;
        }

        public FluentConventionConfiguration ForEntity<T>()
        {
            CreateClassMap(typeof(T));
            return this;
        }

        public FluentConventionConfiguration ForEntitiesInCurrentAssembly(params string[] namespaces)
        {
            foreach (var type in Assembly.GetCallingAssembly().GetExportedTypes())
            {
                if (namespaces != null &&
                    namespaces.Length > 0 &&
                    namespaces.All(n => n != type.Namespace))
                {
                    continue;
                }

                CreateClassMap(type);
            }

            return this;
        }

        public FluentConventionConfiguration ForEntitiesInAssembly(Assembly assembly, params string[] namespaces)
        {
            foreach (var type in assembly.GetExportedTypes())
            {
                if (namespaces != null &&
                    namespaces.Length > 0 &&
                    namespaces.All(n => n != type.Namespace))
                {
                    continue;
                }

                CreateClassMap(type);
            }

            return this;
        }

        private void CreateClassMap(Type entityType)
        {
            var classMap = _mappingConfig.GetMap(entityType);
            UpdateMapProperties(entityType, classMap as IInternalClassMapper);
        }

        private void UpdateMapProperties(Type type, IInternalClassMapper classMap)
        {
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                foreach (var config in _convention.ConventionConfigs)
                {
                    if (!config.PropertyPredicates.All(p => p(property)))
                        continue;

                    if (PropertyMapFilter.Filter(type, property))
                        continue;

                    if (!string.IsNullOrEmpty(config.PropertyConvention.ColumnNameValue))
                    {
                        var propertyMap = classMap.InternalGetPropertyMap(property);
                        ChangeColumnName(propertyMap, config.PropertyConvention.ColumnNameValue);
                    }
                    else if (!string.IsNullOrEmpty(config.PropertyConvention.PrefixValue))
                    {
                        var propertyMap = classMap.InternalGetPropertyMap(property);
                        ChangeColumnName(propertyMap, $"{config.PropertyConvention.PrefixValue}{propertyMap.ColumnName}");
                    }
                    else if (config.PropertyConvention.PropertyTransformer != null)
                    {
                        var propertyMap = classMap.InternalGetPropertyMap(property);
                        ChangeColumnName(propertyMap, config.PropertyConvention.PropertyTransformer(propertyMap.ColumnName));
                    }
                }
            }

            void ChangeColumnName(PropertyMap map, string newColumnName)
            {
                if (map == null || string.IsNullOrWhiteSpace(newColumnName)) return;
                if (map.IsIgnoreConvention) return;
                map.ToColumn(newColumnName);
            }
        }
    }
}