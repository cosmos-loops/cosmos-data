using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// ReSharper disable InconsistentNaming

namespace Cosmos.Data.Statements
{
    /// <summary>
    /// A base dialect class.
    /// </summary>
    public abstract class SQLDialectBase : ISQLDialect
    {
        /// <summary>
        /// Gets dialect's name.
        /// </summary>
        public abstract string DialectName { get; }

        /// <summary>
        /// Gets open quote.
        /// </summary>
        public virtual char OpenQuote => '"';

        /// <summary>
        /// Gets close quote.
        /// </summary>
        public virtual char CloseQuote => '"';

        /// <summary>
        /// Gets batch seperator.
        /// </summary>
        public virtual string BatchSeperator => $";{Environment.NewLine}";

        /// <summary>
        /// To flat support multiple statements or not.
        /// </summary>
        public virtual bool SupportsMultipleStatements => true;

        /// <summary>
        /// Gets parameter prefix.
        /// </summary>
        public virtual char ParameterPrefix => '@';

        /// <summary>
        /// Gets empty expression.
        /// </summary>
        public virtual string EmptyExpression => "1=1";

        /// <summary>
        /// Gets table name.
        /// </summary>
        /// <param name="schemaName">Schema name.</param>
        /// <param name="tableName">Table name.</param>
        /// <param name="alias">Table alias.</param>
        /// <returns>Table name.</returns>
        public virtual string GetTableName(string schemaName, string tableName, string alias)
        {
            if (string.IsNullOrWhiteSpace(tableName))
            {
                throw new ArgumentNullException(nameof(tableName), "TableName cannot be null or empty.");
            }

            var sb = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(schemaName))
            {
                sb.Append($"{QuoteString(schemaName)}.");
            }

            sb.Append(QuoteString(tableName));

            if (!string.IsNullOrWhiteSpace(alias))
            {
                sb.Append($" AS {QuoteString(alias)}");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Gets column name.
        /// </summary>
        /// <param name="prefix">Column prefix.</param>
        /// <param name="columnName">Column name.</param>
        /// <param name="alias">Column alias.</param>
        /// <returns>Column name.</returns>
        public virtual string GetColumnName(string prefix, string columnName, string alias)
        {
            if (string.IsNullOrWhiteSpace(columnName))
            {
                throw new ArgumentNullException(nameof(columnName), "ColumnName cannot be null or empty.");
            }

            var sb = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(prefix))
            {
                sb.Append($"{QuoteString(prefix)}.");
            }

            sb.Append(QuoteString(columnName));

            if (!string.IsNullOrWhiteSpace(alias))
            {
                sb.Append($" AS {QuoteString(alias)}");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Gets identity sql.
        /// </summary>
        /// <param name="tableName">Table name.</param>
        /// <returns>Identity sql.</returns>
        public abstract string GetIdentitySql(string tableName);

        /// <summary>
        /// Gets paging sql.
        /// </summary>
        /// <param name="sql">Sql text.</param>
        /// <param name="pageNumber">Page number.</param>
        /// <param name="pageSize">Page size.</param>
        /// <param name="parameters">Parameters.</param>
        /// <returns>Paging sql.</returns>
        public abstract string GetPagingSql(string sql, int pageNumber, int pageSize, IDictionary<string, object> parameters);

        /// <summary>
        /// Gets set sql.
        /// </summary>
        /// <param name="sql">Sql text.</param>
        /// <param name="firstResult">Row number of the first result.</param>
        /// <param name="maxResults">Row number of the max result.</param>
        /// <param name="parameters">Parameters.</param>
        /// <returns>Set Sql.</returns>
        public abstract string GetSetSql(string sql, int firstResult, int maxResults, IDictionary<string, object> parameters);

        /// <summary>
        /// To flag is Quoted or not.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual bool IsQuoted(string value)
        {
            if (value.Trim()[0] == OpenQuote)
            {
                return value.Trim().Last() == CloseQuote;
            }

            return false;
        }

        /// <summary>
        /// Gets quoted string.
        /// </summary>
        /// <param name="value">Original value.</param>
        /// <returns>Quoted value.</returns>
        public virtual string QuoteString(string value)
        {
            if (IsQuoted(value) || value == "*")
            {
                return value;
            }

            return $"{OpenQuote}{value.Trim()}{CloseQuote}";
        }

        /// <summary>
        /// Gets unquoted string.
        /// </summary>
        /// <param name="value">Quoted value.</param>
        /// <returns>Unquoted value.</returns>
        public virtual string UnQuoteString(string value)
        {
            return IsQuoted(value) ? value.Substring(1, value.Length - 1) : value;
        }
    }
}