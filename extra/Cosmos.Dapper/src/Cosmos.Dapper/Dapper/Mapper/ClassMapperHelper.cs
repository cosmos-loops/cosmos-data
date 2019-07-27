using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using AspectCore.Extensions.Reflection;
using Cosmos.Dapper.Core.Mapping;
using Cosmos.Dapper.EntityMapping;

namespace Dapper.Mapper
{
    internal static class ClassMapperHelper
    {
        private static IList<Assembly> _mapperAssemblies;

        private static Dictionary<Type, KeyType> _keyTypeMappings = new Dictionary<Type, KeyType>
        {
            {typeof(byte), KeyType.Identity}, {typeof(byte?), KeyType.Identity},
            {typeof(sbyte), KeyType.Identity}, {typeof(sbyte?), KeyType.Identity},
            {typeof(short), KeyType.Identity}, {typeof(short?), KeyType.Identity},
            {typeof(ushort), KeyType.Identity}, {typeof(ushort?), KeyType.Identity},
            {typeof(int), KeyType.Identity}, {typeof(int?), KeyType.Identity},
            {typeof(uint), KeyType.Identity}, {typeof(uint?), KeyType.Identity},
            {typeof(long), KeyType.Identity}, {typeof(long?), KeyType.Identity},
            {typeof(ulong), KeyType.Identity}, {typeof(ulong?), KeyType.Identity},
            {typeof(BigInteger), KeyType.Identity}, {typeof(BigInteger?), KeyType.Identity},
            {typeof(Guid), KeyType.Guid}, {typeof(Guid?), KeyType.Guid},
        };

        public static IList<Assembly> GetMapperAssemblies()
        {
            if (_mapperAssemblies != null)
                return _mapperAssemblies;

            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Concat(GetAllUnlinkedAssemblies());

            _mapperAssemblies = AssemblyFilter(assemblies).ToList();

            return _mapperAssemblies;
        }

        private static IEnumerable<Assembly> AssemblyFilter(IEnumerable<Assembly> assemblies)
            => assemblies.Where(assembly =>
                assembly.FullName != typeof(IClassMap<>).Assembly.FullName &&
                (
                    assembly.GetTypes().Any(m => m.GetInterface(typeof(IClassMap<>).FullName) != null) ||
                    assembly.GetTypes().Any(m => m.GetInterface(typeof(IMap).FullName) != null)
                )
            );

        private static IEnumerable<Assembly> GetAllUnlinkedAssemblies()
        {
            var directoryRoot = new DirectoryInfo(Directory.GetCurrentDirectory());
            var files = directoryRoot.GetFiles("*.dll", SearchOption.AllDirectories);
            foreach (var file in files)
                yield return Assembly.LoadFrom(file.FullName);
        }

        public static void CallForEntity<T>(T entity, Action<T> caller, bool flag = true) where T : class
        {
            if (flag)
            {
                caller?.Invoke(entity);
            }
        }

        public static void CallForEntity<T>(IEnumerable<T> entities, Action<T> caller, bool flag = true) where T : class
        {
            if (caller == null || !flag)
                return;

            foreach (var entity in entities)
                CallForEntity(entity, caller);
        }

        public static Dictionary<Type, KeyType> GetKeyTypeMapping() => _keyTypeMappings;

        public static (string SchemaName, bool CaseSensitive) GetSchemaInfo(IClassMap classMap) => GetSchemaInfo(classMap.EntityType);

        public static (string, bool) GetSchemaInfo(Type type)
        {
            var attr = type.GetReflector().GetCustomAttribute<SchemaAttribute>();
            return (attr?.Name ?? string.Empty, attr?.CaseSensitive ?? true);
        }

        public static (string TableName, bool CaseSensitive) GetTableInfo(IClassMap classMap) => GetTableInfo(classMap.EntityType);

        public static (string, bool) GetTableInfo(Type type)
        {
            var attr = type.GetReflector().GetCustomAttribute<TableAttribute>();
            return (attr?.Name ?? string.Empty, attr?.CaseSensitive ?? true);
        }
    }
}