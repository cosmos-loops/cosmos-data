namespace Cosmos.Data.Statements
{
    /// <summary>
    /// Sql sort.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class SQLSort : ISQLSort
    {
        /// <summary>
        /// Create a new instance of <see cref="SQLSort"/>
        /// </summary>
        /// <param name="index"></param>
        /// <param name="field"></param>
        /// <param name="sortType"></param>
        public SQLSort(int index, string field, SQLSortType sortType)
        {
            Index = index;
            FieldName = field;
            SortType = sortType;
        }

        /// <summary>
        /// Index of sort.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Field name.
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// Sort type.
        /// </summary>
        public SQLSortType SortType { get; set; }
    }
}