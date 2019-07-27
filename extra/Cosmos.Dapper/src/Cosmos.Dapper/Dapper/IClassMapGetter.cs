using System;
using System.Collections.Generic;
using Dapper.Mapper;

namespace Dapper
{
    public interface IClassMapGetter
    {
        IReadOnlyDictionary<Type, IClassMap> ClassMappers { get; }

        IClassMap GetMap(Type entityType);
    }
}