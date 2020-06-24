using System.Data;
using System.Data.Common;

namespace Cosmos.Data.Core.Transaction
{
    /// <summary>
    /// Null Transaction
    /// </summary>
    public sealed class NullDbTransaction : DbTransaction
    {
        /// <summary>
        /// Gets instance of <see cref="NullDbTransaction"/>
        /// </summary>
        public static NullDbTransaction Instance => new NullDbTransaction();

        private NullDbTransaction() { }

        /// <summary>
        /// Gets null DbConnection
        /// </summary>
        protected override DbConnection DbConnection => null;

        /// <summary>
        /// Commit.
        /// </summary>
        public override void Commit() { }

        /// <summary>
        /// Rollback.
        /// </summary>
        public override void Rollback() { }

        /// <summary>
        /// Isolation level
        /// </summary>
        public override IsolationLevel IsolationLevel => IsolationLevel.ReadCommitted;
    }
}