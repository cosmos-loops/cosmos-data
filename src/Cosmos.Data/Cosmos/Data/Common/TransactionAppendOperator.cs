using System;

namespace Cosmos.Data.Common
{
    /// <summary>
    /// Transaction append operator
    /// </summary>
    public sealed class TransactionAppendOperator
    {
        /// <summary>
        /// Before commit
        /// </summary>
        public Action BeforeCommit { get; set; }

        /// <summary>
        /// After commit
        /// </summary>
        public Action AfterCommit { get; set; }

        /// <summary>
        /// Before rollback
        /// </summary>
        public Action BeforeRollback { get; set; }

        /// <summary>
        /// After rollback
        /// </summary>
        public Action AfterRollback { get; set; }

        /// <summary>
        /// Before transaction notify
        /// </summary>
        public Action<ITransactionWrapper, BeforeTransNotifyArgs> BeforeTransNotify { get; set; }

        /// <summary>
        /// After transaction notify
        /// </summary>
        public Action<ITransactionWrapper, AfterTransNotifyArgs> AfterTransNotify { get; set; }
    }
}