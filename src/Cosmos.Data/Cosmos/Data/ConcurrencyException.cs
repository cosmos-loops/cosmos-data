using System;

namespace Cosmos.Data
{
    public class ConcurrencyException : CosmosException
    {
        private const string FLAG = "__COSMOS_CONCURRENCY";

        private const string ERROR_MESSAGE = "Concurrency Exception";

        private const long ERROR_CODE = 7001;

        private const long EXTEND_ERROR_CODE = 7002;

        public ConcurrencyException() : this(ERROR_CODE, ERROR_MESSAGE, FLAG) { }

        public ConcurrencyException(Exception exception) : this(ERROR_CODE, ERROR_MESSAGE, FLAG, exception) { }

        public ConcurrencyException(string message, Exception exception = null) : this(ERROR_CODE, message, FLAG, exception) { }

        public ConcurrencyException(string message, string flag, Exception exception = null) : this(ERROR_CODE, message, flag, exception) { }

        public ConcurrencyException(long errorCode, string message, Exception exception = null) : this(errorCode, message, FLAG, exception) { }

        public ConcurrencyException(long errorCode, string message, string flag, Exception exception = null) : base(errorCode, message, flag, exception) { }
    }
}