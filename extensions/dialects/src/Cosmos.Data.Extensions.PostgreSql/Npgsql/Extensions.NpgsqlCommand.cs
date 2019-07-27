﻿// Description: C# Extension Methods | Enhance the .NET Framework and .NET Core with over 1000 extension methods.
// Website & Documentation: https://csharp-extension.com/
// Issues: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License (MIT): https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: https://zzzprojects.com/
// Copyright © ZZZ Projects Inc. All rights reserved.

using System.Data;

namespace Npgsql
{
    public static partial class MySqlClientExtensions
    {
        public static DataSet ExecuteDataSet(this NpgsqlCommand @this)
        {
            var ds = new DataSet();
            using (var dataAdapter = new NpgsqlDataAdapter(@this))
            {
                dataAdapter.Fill(ds);
            }

            return ds;
        }

        public static DataTable ExecuteDataTable(this NpgsqlCommand @this)
        {
            var dt = new DataTable();
            using (var dataAdapter = new NpgsqlDataAdapter(@this))
            {
                dataAdapter.Fill(dt);
            }

            return dt;
        }
    }
}