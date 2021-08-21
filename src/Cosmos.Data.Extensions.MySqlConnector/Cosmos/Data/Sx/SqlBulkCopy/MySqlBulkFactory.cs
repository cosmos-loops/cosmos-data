using System;
using Cosmos.Text;
using MySqlConnector;
using CosmosWayCopy = Cosmos.Data.Sx.SqlBulkCopy.MySqlBulkCopy;
using OriginalWayCopy = MySqlConnector.MySqlBulkCopy;

namespace Cosmos.Data.Sx.SqlBulkCopy
{
    /// <summary>
    /// The factory for MySqlBulkCopy
    /// </summary>
    public static class MySqlBulkFactory
    {
        /// <summary>
        /// Create a Cosmos Way MySql bulk copy object.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="optionsAct"></param>
        /// <returns></returns>
        public static CosmosWayCopy CreateInCosmosWay(MySqlConnection connection, Action<MySqlBulkCopyOptions> optionsAct = null)
        {
            var options = new MySqlBulkCopyOptions();
            optionsAct?.Invoke(options);

            var copy = new CosmosWayCopy(connection);
            if (options.BulkCopyTimeout.HasValue)
                copy.BulkCopyTimeout = options.BulkCopyTimeout.Value;
            if (options.DestinationTableName.IsNullOrWhiteSpace())
                copy.DestinationTableName = options.DestinationTableName;
            return copy;
        }

        /// <summary>
        /// Create an original way MySql bulk copy object.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <param name="optionsAct"></param>
        /// <returns></returns>
        public static OriginalWayCopy CreateInOriginalWay(MySqlConnection connection, MySqlTransaction transaction = null, Action<MySqlBulkCopyOptions> optionsAct = null)
        {
            var options = new MySqlBulkCopyOptions();
            optionsAct?.Invoke(options);

            var copy = new OriginalWayCopy(connection, transaction);
            if (options.BulkCopyTimeout.HasValue)
                copy.BulkCopyTimeout = options.BulkCopyTimeout.Value;
            if (options.DestinationTableName.IsNullOrWhiteSpace())
                copy.DestinationTableName = options.DestinationTableName;
            return copy;
        }
    }
}