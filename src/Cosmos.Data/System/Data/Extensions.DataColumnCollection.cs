namespace System.Data
{
    /// <summary>
    /// Extensions for <see cref="DataColumnCollection"/>
    /// </summary>
    public static class DataColumnCollectionExtensions
    {
        /// <summary>
        /// Add range
        /// </summary>
        /// <param name="this"></param>
        /// <param name="columns"></param>
        public static void AddRange(this DataColumnCollection @this, params string[] columns)
        {
            foreach (var column in columns)
            {
                @this.Add(column);
            }
        }
    }
}