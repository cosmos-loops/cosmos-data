namespace Cosmos.Data.Sx.MySql.SqlBulkCopy
{
    /// <summary>
    /// Options for MySql bulk copy
    /// </summary>
    public class MySqlBulkCopyOptions
    {
        /// <summary>
        /// BulkCopy timeout
        /// </summary>
        public int? BulkCopyTimeout { get; set; }
        
        /// <summary>
        /// Destination TableName
        /// </summary>
        public string DestinationTableName { get; set; }
    }
}