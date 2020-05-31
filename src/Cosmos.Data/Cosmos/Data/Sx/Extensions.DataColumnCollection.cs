using System.Data;

namespace Cosmos.Data.Sx
{
    /// <summary>
    /// Extensions for <see cref="DataColumnCollection"/>
    /// </summary>
    public static class DataColumnCollectionExtensions
    {
        /// <summary>
        /// Add range
        /// </summary>
        /// <param name="coll"></param>
        /// <param name="columns"></param>
        public static void AddRange(this DataColumnCollection coll, params string[] columns)
        {
            coll.CheckNull(nameof(coll));
            
            foreach (var column in columns)
            {
                coll.Add(column);
            }
        }
    }
}