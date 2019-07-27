using System;

namespace Cosmos.Data.Transaction
{
    public sealed class TransactionAppendOperator
    {
        public Action BeforeCommit { get; set; }
        
        public Action AfterCommit { get; set; }
        
        public Action BeforeRollback { get; set; }
        
        public Action AfterRollback { get; set; }
    }
}