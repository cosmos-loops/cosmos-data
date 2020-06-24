using System.Collections.Generic;

// ReSharper disable InconsistentNaming
namespace Cosmos.Data.Statements
{
    /// <summary>
    /// An interface of Sql Dialect.
    /// </summary>
    public interface ISQLDialect
    {
        /// <summary>
        /// Gets dialect's name.
        /// </summary>
        string DialectName { get; }

        /// <summary>
        /// Gets open quote.
        /// </summary>
        char OpenQuote { get; }

        /// <summary>
        /// Gets close quote.
        /// </summary>
        char CloseQuote { get; }

        /// <summary>
        /// Gets batch seperator.
        /// </summary>
        string BatchSeperator { get; }

        /// <summary>
        /// To flat support multiple statements or not.
        /// </summary>
        bool SupportsMultipleStatements { get; }

        /// <summary>
        /// Gets parameter prefix.
        /// </summary>
        char ParameterPrefix { get; }

        /// <summary>
        /// Gets empty expression.
        /// </summary>
        string EmptyExpression { get; }

        /// <summary>
        /// Gets table name.
        /// </summary>
        /// <param name="schemaName">Schema name.</param>
        /// <param name="tableName">Table name.</param>
        /// <param name="alias">Table alias.</param>
        /// <returns>Table name.</returns>
        string GetTableName(string schemaName, string tableName, string alias);

        /// <summary>
        /// Gets column name.
        /// </summary>
        /// <param name="prefix">Column prefix.</param>
        /// <param name="columnName">Column name.</param>
        /// <param name="alias">Column alias.</param>
        /// <returns>Column name.</returns>
        string GetColumnName(string prefix, string columnName, string alias);

        /// <summary>
        /// Gets identity sql.
        /// </summary>
        /// <param name="tableName">Table name.</param>
        /// <returns>Identity sql.</returns>
        string GetIdentitySql(string tableName);

        /// <summary>
        /// Gets paging sql.
        /// </summary>
        /// <param name="sql">Sql text.</param>
        /// <param name="pageNumber">Page number.</param>
        /// <param name="pageSize">Page size.</param>
        /// <param name="parameters">Parameters.</param>
        /// <returns>Paging sql.</returns>
        string GetPagingSql(string sql, int pageNumber, int pageSize, IDictionary<string, object> parameters);

        /// <summary>
        /// Gets set sql.
        /// </summary>
        /// <param name="sql">Sql text.</param>
        /// <param name="firstResult">Row number of the first result.</param>
        /// <param name="maxResults">Row number of the max result.</param>
        /// <param name="parameters">Parameters.</param>
        /// <returns>Set Sql.</returns>
        string GetSetSql(string sql, int firstResult, int maxResults, IDictionary<string, object> parameters);

        /// <summary>
        /// To flag is Quoted or not.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool IsQuoted(string value);

        /// <summary>
        /// Gets quoted string.
        /// </summary>
        /// <param name="value">Original value.</param>
        /// <returns>Quoted value.</returns>
        string QuoteString(string value);
    }
}