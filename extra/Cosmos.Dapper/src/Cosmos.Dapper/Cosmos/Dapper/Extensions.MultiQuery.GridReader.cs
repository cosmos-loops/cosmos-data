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
    public static class MultiGridReaderExtensions
    {
        public static IEnumerable<T1> Read<T1, T2>(this SqlMapper.GridReader gridReader, string splitOn = "id", bool buffered = true)
            => gridReader.Read(MappingCache<T1, T2>.Map, splitOn, buffered);

        public static IEnumerable<T1> Read<T1, T2, T3>(this SqlMapper.GridReader gridReader, string splitOn = "id", bool buffered = true)
            => gridReader.Read(MappingCache<T1, T2, T3>.Map, splitOn, buffered);

        public static IEnumerable<T1> Read<T1, T2, T3, T4>(this SqlMapper.GridReader gridReader, string splitOn = "id", bool buffered = true)
            => gridReader.Read(MappingCache<T1, T2, T3, T4>.Map, splitOn, buffered);

        public static IEnumerable<T1> Read<T1, T2, T3, T4, T5>(this SqlMapper.GridReader gridReader, string splitOn = "id", bool buffered = true)
            => gridReader.Read(MappingCache<T1, T2, T3, T4, T5>.Map, splitOn, buffered);

        public static IEnumerable<T1> Read<T1, T2, T3, T4, T5, T6>(this SqlMapper.GridReader gridReader, string splitOn = "id", bool buffered = true)
            => gridReader.Read(MappingCache<T1, T2, T3, T4, T5, T6>.Map, splitOn, buffered);

        public static IEnumerable<T1> Read<T1, T2, T3, T4, T5, T6, T7>(this SqlMapper.GridReader gridReader, string splitOn = "id", bool buffered = true)
            => gridReader.Read(MappingCache<T1, T2, T3, T4, T5, T6, T7>.Map, splitOn, buffered);
    }
}