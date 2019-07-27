using System.Collections.Generic;
using Dapper;
using Dapper.Mapper;

namespace Cosmos.Data.Statements
{
    // ReSharper disable once InconsistentNaming
    public interface ISQLGenerator
    {
        IDapperMappingConfig Configuration { get; }

        SQLConvertResult Select(IClassMap classMap, ISQLPredicate predicate, SQLSortSet sort, IDictionary<string, object> parameters);

        SQLConvertResult SelectPaged(IClassMap classMap, ISQLPredicate predicate, SQLSortSet sort, int pageNumber, int pageSize, IDictionary<string, object> parameters);

        SQLConvertResult SelectSet(IClassMap classMap, ISQLPredicate predicate, SQLSortSet sort, int limitFrom, int limitTo, IDictionary<string, object> parameters);

        SQLConvertResult Count(IClassMap classMap, ISQLPredicate predicate, IDictionary<string, object> parameters);

        SQLConvertResult Insert(IClassMap classMap);

        SQLConvertResult Update(IClassMap classMap, ISQLPredicate predicate, IDictionary<string, object> parameters, bool ignoreAllKeyProperties);

        SQLConvertResult Delete(IClassMap classMap, ISQLPredicate predicate, IDictionary<string, object> parameters);

        string IdentitySql(IClassMap classMap);

        string GetTableName(IClassMap classMap);

        string GetColumnName(IClassMap map, IPropertyMap property, bool includeAlias);

        string GetColumnName(IClassMap map, string propertyName, bool includeAlias);

        bool SupportsMultipleStatements();
    }
}