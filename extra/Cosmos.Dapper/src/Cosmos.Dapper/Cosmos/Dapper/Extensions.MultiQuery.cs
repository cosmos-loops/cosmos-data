/*
 * Copyright 2018 Arjen Post
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Cosmos.Dapper.Core.Mapping.Cache;
using Dapper;

/*
 * Reference to:
 *     dotarj/Dapper.Mapper
 *     Author: Arjen Post
 *     URL: https://github.com/dotarj/Dapper.Mapper
 *     Apache License 2.0
 */

namespace Cosmos.Dapper
{
    public static class MultiQueryExtensions
    {
        public static IEnumerable<T1> Query<T1, T2>(
            this IDbConnection connection, string sql, dynamic param = null, IDbTransaction transaction = null,
            bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
            => SqlMapper.Query<T1, T2, T1>(connection, sql, MappingCache<T1, T2>.Map,
                param, transaction, buffered, splitOn, commandTimeout, commandType);

        public static IEnumerable<T1> Query<T1, T2, T3>(
            this IDbConnection connection, string sql, dynamic param = null, IDbTransaction transaction = null,
            bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
            => SqlMapper.Query<T1, T2, T3, T1>(connection, sql, MappingCache<T1, T2, T3>.Map,
                param, transaction, buffered, splitOn, commandTimeout, commandType);

        public static IEnumerable<T1> Query<T1, T2, T3, T4>(
            this IDbConnection connection, string sql, dynamic param = null, IDbTransaction transaction = null,
            bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
            => SqlMapper.Query<T1, T2, T3, T4, T1>(connection, sql, MappingCache<T1, T2, T3, T4>.Map,
                param, transaction, buffered, splitOn, commandTimeout, commandType);

        public static IEnumerable<T1> Query<T1, T2, T3, T4, T5>(
            this IDbConnection connection, string sql, dynamic param = null, IDbTransaction transaction = null,
            bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
            => SqlMapper.Query<T1, T2, T3, T4, T5, T1>(connection, sql, MappingCache<T1, T2, T3, T4, T5>.Map,
                param, transaction, buffered, splitOn, commandTimeout, commandType);

        public static IEnumerable<T1> Query<T1, T2, T3, T4, T5, T6>(
            this IDbConnection connection, string sql, dynamic param = null, IDbTransaction transaction = null,
            bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
            => SqlMapper.Query<T1, T2, T3, T4, T5, T6, T1>(connection, sql, MappingCache<T1, T2, T3, T4, T5, T6>.Map,
                param, transaction, buffered, splitOn, commandTimeout, commandType);

        public static IEnumerable<T1> Query<T1, T2, T3, T4, T5, T6, T7>(
            this IDbConnection connection, string sql, dynamic param = null, IDbTransaction transaction = null,
            bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
            => SqlMapper.Query<T1, T2, T3, T4, T5, T6, T7, T1>(connection, sql, MappingCache<T1, T2, T3, T4, T5, T6, T7>.Map,
                param, transaction, buffered, splitOn, commandTimeout, commandType);

        public static Task<IEnumerable<T1>> QueryAsync<T1, T2>(
            this IDbConnection connection, string sql, dynamic param = null, IDbTransaction transaction = null,
            bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
            => SqlMapper.QueryAsync<T1, T2, T1>(connection, sql, MappingCache<T1, T2>.Map,
                param, transaction, buffered, splitOn, commandTimeout, commandType);

        public static Task<IEnumerable<T1>> QueryAsync<T1, T2, T3>(
            this IDbConnection connection, string sql, dynamic param = null, IDbTransaction transaction = null,
            bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
            => SqlMapper.QueryAsync<T1, T2, T3, T1>(connection, sql, MappingCache<T1, T2, T3>.Map,
                param, transaction, buffered, splitOn, commandTimeout, commandType);

        public static Task<IEnumerable<T1>> QueryAsync<T1, T2, T3, T4>(
            this IDbConnection connection, string sql, dynamic param = null, IDbTransaction transaction = null,
            bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
            => SqlMapper.QueryAsync<T1, T2, T3, T4, T1>(connection, sql, MappingCache<T1, T2, T3, T4>.Map,
                param, transaction, buffered, splitOn, commandTimeout, commandType);

        public static Task<IEnumerable<T1>> QueryAsync<T1, T2, T3, T4, T5>(
            this IDbConnection connection, string sql, dynamic param = null, IDbTransaction transaction = null,
            bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
            => SqlMapper.QueryAsync<T1, T2, T3, T4, T5, T1>(connection, sql, MappingCache<T1, T2, T3, T4, T5>.Map,
                param, transaction, buffered, splitOn, commandTimeout, commandType);

        public static Task<IEnumerable<T1>> QueryAsync<T1, T2, T3, T4, T5, T6>(
            this IDbConnection connection, string sql, dynamic param = null, IDbTransaction transaction = null,
            bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
            => SqlMapper.QueryAsync<T1, T2, T3, T4, T5, T6, T1>(connection, sql, MappingCache<T1, T2, T3, T4, T5, T6>.Map,
                param, transaction, buffered, splitOn, commandTimeout, commandType);

        public static Task<IEnumerable<T1>> QueryAsync<T1, T2, T3, T4, T5, T6, T7>(
            this IDbConnection connection, string sql, dynamic param = null, IDbTransaction transaction = null,
            bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
            => SqlMapper.QueryAsync<T1, T2, T3, T4, T5, T6, T7, T1>(connection, sql, MappingCache<T1, T2, T3, T4, T5, T6, T7>.Map,
                param, transaction, buffered, splitOn, commandTimeout, commandType);
    }
}