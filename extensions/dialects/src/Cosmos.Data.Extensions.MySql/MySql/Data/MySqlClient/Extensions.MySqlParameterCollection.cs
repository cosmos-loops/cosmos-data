// Description: C# Extension Methods | Enhance the .NET Framework and .NET Core with over 1000 extension methods.
// Website & Documentation: https://csharp-extension.com/
// Issues: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License (MIT): https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: https://zzzprojects.com/
// Copyright © ZZZ Projects Inc. All rights reserved.

using System.Collections.Generic;

namespace MySql.Data.MySqlClient
{
    /// <summary>
    /// Extensions for <see cref="MySqlClient"/>
    /// </summary>
    public static partial class MySqlClientExtensions
    {
        /// <summary>
        /// Add range with value
        /// </summary>
        /// <param name="this"></param>
        /// <param name="values"></param>
        public static void AddRangeWithValue(this MySqlParameterCollection @this, Dictionary<string, object> values)
        {
            foreach (var keyValuePair in values)
            {
                @this.AddWithValue(keyValuePair.Key, keyValuePair.Value);
            }
        }
    }
}