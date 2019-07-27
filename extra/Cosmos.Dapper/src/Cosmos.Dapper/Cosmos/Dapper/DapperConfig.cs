using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using Cosmos.Dapper.Core.Mapping;
using Cosmos.Dapper.Core.Mapping.Filters;
using Cosmos.Dapper.Core.SqlKata;
using Cosmos.Data.Statements;
using Cosmos.IdUtils;
using Dapper;
using Dapper.Mapper;
using SqlKata.Compilers;

namespace Cosmos.Dapper
{
    public class DapperConfig : IDapperMappingConfig, IInternalDapperMappingConfig, IClassMapGetter
    {
        private readonly ConcurrentDictionary<Type, IClassMap> _classMappers;
        private readonly ISqlKataCompilerCreator _compilerCreator;

        public DapperConfig(ISQLDialect sqlDialect, ISqlKataCompilerCreator compilerCreator, DapperOptions options, bool strict)
            : this(typeof(AutoClassMap<>), null, sqlDialect, compilerCreator, options, strict) { }

        public DapperConfig(
            Type defaultMapper,
            IList<Assembly> mappingAssemblies,
            ISQLDialect sqlDialect,
            ISqlKataCompilerCreator compilerCreator,
            DapperOptions options,
            bool strict)
        {
            _classMappers = new ConcurrentDictionary<Type, IClassMap>();
            _compilerCreator = compilerCreator ?? throw new ArgumentNullException(nameof(compilerCreator));
            Options = options ?? throw new ArgumentNullException(nameof(options));
            IsStrictMode = strict;

            DefaultMapper = defaultMapper;
            MappingAssemblies = mappingAssemblies ?? ClassMapperHelper.GetMapperAssemblies();
            Dialect = sqlDialect;
        }

        public IReadOnlyDictionary<Type, IClassMap> ClassMappers => new ReadOnlyDictionary<Type, IClassMap>(_classMappers);

        public Type DefaultMapper { get; }

        public IList<Assembly> MappingAssemblies { get; }

        public ISQLDialect Dialect { get; }

        #region GetMap

        public IClassMap GetMap(Type entityType)
        {
            if (!_classMappers.TryGetValue(entityType, out var map))
            {
                var mapType = GetMapType(entityType);
                if (mapType == null)
                    mapType = DefaultMapper.MakeGenericType(entityType);

                map = Types.CreateInstance<IClassMap>(mapType);
                _classMappers[entityType] = map;
            }

            return map;
        }

        public IClassMap<T> GetMap<T>() where T : class => GetMap(typeof(T)) as IClassMap<T>;

        internal IInternalClassMapper GetInternalMap(Type entityType) => GetMap(entityType) as IInternalClassMapper;

        internal IInternalClassMapper<T> GetInternalMap<T>() where T : class => GetMap<T>() as IInternalClassMapper<T>;

        protected virtual Type GetMapType(Type entityType, bool fluentMapMode = false)
        {
            var ret = ClassMapFilter.Filter(entityType, entityType.Assembly, fluentMapMode);
            if (ret != null) return ret;

            foreach (var mappingAssembly in MappingAssemblies)
            {
                ret = ClassMapFilter.Filter(entityType, mappingAssembly, fluentMapMode);
                if (ret != null) return ret;
            }

            return null;
        }

        #endregion

        #region SetMap

        public void SetMap(IClassMap classMap)
        {
            if (classMap == null)
                throw new ArgumentNullException(nameof(classMap));
            _classMappers.TryAdd(classMap.EntityType, classMap);
        }

        #endregion

        #region GetSqlKataCompiler

        public Compiler GetCompiler() => _compilerCreator.Create();

        #endregion

        public void ClearCache() => _classMappers.Clear();

        public Guid GetNextGuid() => GuidProvider.Create(CombStyle.NormalStyle);

        public bool IsStrictMode { get; }

        public DapperOptions Options { get; }
    }
}