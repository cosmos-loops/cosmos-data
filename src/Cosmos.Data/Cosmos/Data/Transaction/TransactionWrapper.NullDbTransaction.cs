using System.Data;
using System.Data.Common;

namespace Cosmos.Data.Transaction
{
    public sealed class NullDbTransaction : DbTransaction
    {
        public static NullDbTransaction Instance => new NullDbTransaction();

        private NullDbTransaction() { }

        protected override DbConnection DbConnection => null;

        public override void Commit() { }

        public override void Rollback() { }

        public override IsolationLevel IsolationLevel => IsolationLevel.ReadCommitted;
    }
}