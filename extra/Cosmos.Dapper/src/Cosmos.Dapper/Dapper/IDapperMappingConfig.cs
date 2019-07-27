using System;
using System.Collections.Generic;
using System.Reflection;
using Cosmos.Dapper;
using Cosmos.Data.Statements;
using Dapper.Mapper;

namespace Dapper
{
    public interface IDapperMappingConfig
    {
        Type DefaultMapper { get; }

        IList<Assembly> MappingAssemblies { get; }

        ISQLDialect Dialect { get; }

        IClassMap GetMap(Type entityType);

        IClassMap<T> GetMap<T>() where T : class;

        void ClearCache();

        Guid GetNextGuid();

        DapperOptions Options { get; }
    }
}