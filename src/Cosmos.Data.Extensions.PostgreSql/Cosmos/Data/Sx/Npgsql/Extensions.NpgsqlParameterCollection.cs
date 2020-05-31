﻿using System.Collections.Generic;
using Npgsql;

namespace Cosmos.Data.Sx.Npgsql
{
    /// <summary>
    /// Extensions for Npgsql
    /// </summary>
    public static partial class NpgsqlClientExtensions
    {
        /// <summary>
        /// Add range with value
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="values"></param>
        public static void AddRangeWithValue(this NpgsqlParameterCollection conn, Dictionary<string, object> values)
        {
            conn.CheckNull(nameof(conn));
            
            foreach (var keyValuePair in values)
            {
                conn.AddWithValue(keyValuePair.Key, keyValuePair.Value);
            }
        }
    }
}