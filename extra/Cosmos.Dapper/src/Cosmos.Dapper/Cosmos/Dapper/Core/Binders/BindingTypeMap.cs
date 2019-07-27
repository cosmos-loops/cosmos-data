using System;
using System.Linq;
using Dapper;
using Dapper.Mapper;

namespace Cosmos.Dapper.Core.Binders
{
    public class BindingTypeMap : MultiTypeMap
    {
        public BindingTypeMap(Type entityType, IClassMap classMap)
            : base(
                new CustomPropertyTypeMap(
                    entityType,
                    (type, columnName) => classMap.PropertyMaps.FirstOrDefault(x => x.ColumnName == columnName)?.PropertyInfo),
                new DefaultTypeMap(entityType)) { }
    }
}