namespace Cosmos.Data
{
    /// <summary>
    /// An interface of data source config.
    /// </summary>
    public interface IDataSourceConfig
    {
        /// <summary>
        /// Gets connection string.
        /// </summary>
        string ConnectionString { get; }
    }
}