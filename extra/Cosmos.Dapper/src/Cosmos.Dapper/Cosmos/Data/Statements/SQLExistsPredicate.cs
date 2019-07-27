using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Dapper.Mapper;

namespace Cosmos.Data.Statements
{
    public class SQLExistsPredicate<TSub> : ISQLExistsPredicate where TSub : class
    {
        public string GetSql(ISQLGenerator sqlGenerator, IDictionary<string, object> parameters)
        {
            var mapSub = GetClassMapper(typeof(TSub), sqlGenerator.Configuration);
            var sql = $"({(Not ? "NOT " : string.Empty)}EXISTS (SELECT 1 FROM {sqlGenerator.GetTableName(mapSub)} WHERE {Predicate.GetSql(sqlGenerator, parameters)}))";
            return sql;
        }

        public ISQLPredicate Predicate { get; set; }

        public bool Not { get; set; }

        protected virtual IClassMap GetClassMapper(Type type, IDapperMappingConfig config)
        {
            var map = config.GetMap(type);

            if (map == null)
                throw new NullReferenceException($"Map was not found for '{type}'");

            return map;
        }
    }
}
