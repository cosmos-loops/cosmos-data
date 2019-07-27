using System;
using System.Collections.Generic;
using System.Linq;
using Cosmos.Data.SqlKata;

namespace Cosmos.Data.Statements
{
    // ReSharper disable once InconsistentNaming
    public abstract class SQLBasePredicate : ISQLBasePredicate
    {
        public abstract string GetSql(ISQLGenerator sqlGenerator, IDictionary<string, object> parameters);

        public Type EntityType { get; set; }

        public string PropertyName { get; set; }

        protected virtual string GetColumnName(Type entityType, ISQLGenerator sqlGenerator, string propertyName)
        {
            var map = sqlGenerator.Configuration.GetMap(entityType);
            if (map == null)
                throw new NullReferenceException($"Map was not found for '{entityType}'");

            var propertyMap = map.PropertyMaps.SingleOrDefault(p => p.Name == propertyName);
            if (propertyMap == null)
                throw new NullReferenceException($"'{propertyName}' was not found for '{entityType}'");

            return sqlGenerator.GetColumnName(map, propertyMap, false);
        }
    }
}