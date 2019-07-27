using System;
using System.Collections.Generic;
using System.Linq;
using Cosmos.Dapper.EntityMapping;
using Cosmos.Domain;

namespace Cosmos.Dapper.Core.Mapping
{
    public sealed class EntityMapScanner<TContext> : InstanceScanner<IMap>
        where TContext : class, IDapperContext
    {
        // ReSharper disable once InconsistentNaming
        private const string SKIP_ASSEMBLIES =
            "^System|^Mscorlib|^Netstandard|^Microsoft|^Autofac|^AutoMapper|^EntityFramework|^Newtonsoft|^Castle|^NLog|^Pomelo|^AspectCore|^Xunit|^Nito|^Npgsql|^Exceptionless|^MySqlConnector|^Anonymously Hosted";

        public EntityMapScanner(Type entityMapType) : this(entityMapType, string.Empty) { }

        public EntityMapScanner(Type entityMapType, string limitedAssemblies) : base(entityMapType)
        {
            LimitedInAssemblies = limitedAssemblies;
            DbContextType = typeof(TContext);
            BindingEntityTypes = GetDbSetBodyTypes();
        }

        private Type DbContextType { get; }

        private List<Type> BindingEntityTypes { get; }

        private string LimitedInAssemblies { get; }

        protected override string GetSkipAssembliesNamespaces() => SKIP_ASSEMBLIES;

        protected override string GetLimitedAssembliesNamespaces() => LimitedInAssemblies;

        private List<Type> GetDbSetBodyTypes()
        {
            return DbContextType.GetProperties()
                .Select(t => t.PropertyType)
                .Where(t => t.IsGenericType)
                .Select(s => s.GetGenericArguments()[0])
                .ToList();
        }

        protected override Func<Type, bool> TypeFilter() =>
            t => t.IsClass && t.IsPublic && !t.IsAbstract &&
                 BaseType.IsAssignableFrom(t) &&
                 t.IsNotDefined<IgnoreMapAttribute>() &&
                 t.IsNotDefined<EntityMapIgnoreScanningAttribute>() &&
                 t.IsMatchedEntityMappingRule(BindingEntityTypes);
    }
}