namespace Cosmos.Data.Statements
{
    /// <summary>
    /// Sql sort.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public interface ISQLSort
    {
        /// <summary>
        /// Field name.
        /// </summary>
        string FieldName { get; set; }

        /// <summary>
        /// Sort type.
        /// </summary>
        SQLSortType SortType { get; set; }
    }
}
