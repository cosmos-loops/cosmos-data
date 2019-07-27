/*
 * Copyright 2011 Thad Smith, Page Brooks and contributors
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Text;

/*
 * Reference to:
 *      tmsmith/Dapper-Extensions
 *      Author: Thad Smith
 *      Url: https://github.com/tmsmith/Dapper-Extensions
 *      Apache License 2.0
 *          http://www.apache.org/licenses/LICENSE-2.0
 */

namespace Cosmos.Data.Statements.Dialects
{
    public class SqliteDialect : SQLDialectBase
    {
        public override string DialectName => "Sqlite";
        
        public override string GetIdentitySql(string tableName) => "SELECT LAST_INSERT_ROWID() AS [Id]";

        public override string GetPagingSql(string sql, int pageNumber, int pageSize, IDictionary<string, object> parameters)
            => GetSetSql(sql, pageNumber * pageSize, pageSize, parameters);

        public override string GetSetSql(string sql, int firstResult, int maxResults, IDictionary<string, object> parameters)
        {
            if (string.IsNullOrEmpty(sql))
                throw new ArgumentNullException(nameof(sql));

            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var ret = $"{sql} LIMIT @Offset, @Count";
            parameters.Add("@Offset", firstResult);
            parameters.Add("@Count", maxResults);
            return ret;
        }

        public override string GetColumnName(string prefix, string columnName, string alias)
        {
            if (string.IsNullOrWhiteSpace(columnName))
                throw new ArgumentNullException(nameof(columnName), "columnName cannot be null or empty");

            var ret = new StringBuilder();
            ret.AppendFormat(columnName);
            if (!string.IsNullOrWhiteSpace(alias))
                ret.Append($" AS {QuoteString(alias)}");
            return ret.ToString();
        }
    }
}
