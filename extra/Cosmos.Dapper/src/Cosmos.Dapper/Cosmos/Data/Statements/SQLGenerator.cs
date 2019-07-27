using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Dapper.Internals;
using Dapper.Mapper;

namespace Cosmos.Data.Statements
{
    // ReSharper disable once InconsistentNaming
    public class SQLGenerator : ISQLGenerator
    {
        public SQLGenerator(IDapperMappingConfig config)
        {
            Configuration = config;
        }

        public IDapperMappingConfig Configuration { get; }

        public SQLConvertResult Select(IClassMap classMap, ISQLPredicate predicate, SQLSortSet sort, IDictionary<string, object> parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var sql = Sql.Select(BuildSelectColumns(classMap))
                .From(GetTableName(classMap))
                .Where(predicate != null, () => predicate.GetSql(this, parameters))
                .OrderBy(sort?.ToStrings(classMap, GetColumnName).AppendStrings())
                .ToString();

            return new SQLConvertResult(sql, parameters);
        }

        public SQLConvertResult SelectPaged(IClassMap classMap, ISQLPredicate predicate, SQLSortSet sort, int pageNumber, int pageSize, IDictionary<string, object> parameters)
        {
            if (sort == null || !sort.Any())
                throw new ArgumentNullException(nameof(sort), "Sort cannot be null or empty.");

            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var innerSql = Sql.Select(BuildSelectColumns(classMap))
                .From(GetTableName(classMap))
                .Where(predicate != null, () => predicate.GetSql(this, parameters))
                .OrderBy(sort.ToStrings(classMap, GetColumnName).AppendStrings());

            var sql = Configuration.Dialect.GetPagingSql(innerSql.ToString(), pageNumber, pageSize, parameters);

            return new SQLConvertResult(sql, parameters);
        }

        public SQLConvertResult SelectSet(IClassMap classMap, ISQLPredicate predicate, SQLSortSet sort, int limitFrom, int limitTo, IDictionary<string, object> parameters)
        {
            if (sort == null || !sort.Any())
                throw new ArgumentNullException(nameof(sort), "Sort cannot be null or empty.");

            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var innerSql = Sql.Select(BuildSelectColumns(classMap))
                .From(GetTableName(classMap))
                .Where(predicate != null, () => predicate.GetSql(this, parameters))
                .OrderBy(sort.ToStrings(classMap, GetColumnName).AppendStrings());

            var sql = Configuration.Dialect.GetSetSql(innerSql.ToString(), limitFrom, limitTo, parameters);

            return new SQLConvertResult(sql, parameters);
        }

        public SQLConvertResult Count(IClassMap classMap, ISQLPredicate predicate, IDictionary<string, object> parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var sql = Sql.Select()
                 .Count("*").As($"{Configuration.Dialect.OpenQuote}Total{Configuration.Dialect.CloseQuote}")
                 .From(GetTableName(classMap))
                 .Where(predicate != null, () => predicate.GetSql(this, parameters))
                 .ToString();

            return new SQLConvertResult(sql, parameters);
        }

        public SQLConvertResult Insert(IClassMap classMap)
        {
            var columns = classMap.PropertyMaps.Where(p => !(p.Ignored || p.IsReadOnly || p.KeyType == KeyType.Identity || p.KeyType == KeyType.TriggerIdentity));

            if (!columns.Any())
                throw new ArgumentException("No columns were mapped.");

            var columnNames = columns.Select(p => GetColumnName(classMap, p, false));
            var parameters = columns.Select(p => Configuration.Dialect.ParameterPrefix + p.Name);

            var sql = Sql.Insert().Into(GetTableName(classMap)).Columns(columnNames).Values(parameters);

            var triggerIdentityColumn = classMap.PropertyMaps.Where(p => p.KeyType == KeyType.TriggerIdentity).ToList();

            if (triggerIdentityColumn.Count > 0)
            {
                if (triggerIdentityColumn.Count > 1)
                    throw new ArgumentException("TriggerIdentity generator cannot be used with multi-column keys.");

                sql.Append($" RETURNING {triggerIdentityColumn.Select(p => GetColumnName(classMap, p, false)).First()} INTO {Configuration.Dialect.ParameterPrefix}IdOutParam");
            }

            return new SQLConvertResult(sql.ToString(), (IDictionary<string, object>)null);
        }

        public SQLConvertResult Update(IClassMap classMap, ISQLPredicate predicate, IDictionary<string, object> parameters, bool ignoreAllKeyProperties)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var columns = ignoreAllKeyProperties
                ? classMap.PropertyMaps.Where(p => !(p.Ignored || p.IsReadOnly) && p.KeyType == KeyType.NotAKey)
                : classMap.PropertyMaps.Where(p => !(p.Ignored || p.IsReadOnly || p.KeyType == KeyType.Identity || p.KeyType == KeyType.Assigned));

            if (!columns.Any())
                throw new ArgumentException("Co columns were mapped.");

            var setSql = columns.Select(p => $"{GetColumnName(classMap, p, false)} = {Configuration.Dialect.ParameterPrefix}{p.Name}");

            var sql = Sql.Update(GetTableName(classMap)).Set(setSql.AppendStrings()).Where(predicate.GetSql(this, parameters)).ToString();

            return new SQLConvertResult(sql, parameters);
        }

        public SQLConvertResult Delete(IClassMap classMap, ISQLPredicate predicate, IDictionary<string, object> parameters)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var sql = Sql.Delete().From(GetTableName(classMap)).Where(predicate.GetSql(this, parameters)).ToString();

            return new SQLConvertResult(sql, parameters);
        }

        public string IdentitySql(IClassMap classMap)
        {
            return Configuration.Dialect.GetIdentitySql(GetTableName(classMap));
        }

        public string GetTableName(IClassMap classMap)
        {
            return Configuration.Dialect.GetTableName(classMap.SchemaName, classMap.TableName, null);
        }

        public string GetColumnName(IClassMap map, IPropertyMap property, bool includeAlias)
        {
            string alias = null;
            if (property.ColumnName != property.Name && includeAlias)
                alias = property.Name;
            return Configuration.Dialect.GetColumnName(GetTableName(map), property.ColumnName, alias);
        }

        public string GetColumnName(IClassMap map, string propertyName, bool includeAlias)
        {
            var propertyMap = map.PropertyMaps.SingleOrDefault(p => p.Name.Equals(propertyName, StringComparison.InvariantCultureIgnoreCase));
            if (propertyMap == null)
                throw new ArgumentException($"Cloud not find '{propertyName}' in Mapping.");

            return GetColumnName(map, propertyMap, includeAlias);
        }

        public bool SupportsMultipleStatements()
        {
            return Configuration.Dialect.SupportsMultipleStatements;
        }

        public virtual string BuildSelectColumns(IClassMap classMap)
        {
            var columns = classMap.PropertyMaps.Where(p => !p.Ignored).Select(p => GetColumnName(classMap, p, true));
            return columns.AppendStrings();
        }

        private StringBuilder Sql => new StringBuilder();
    }
}
