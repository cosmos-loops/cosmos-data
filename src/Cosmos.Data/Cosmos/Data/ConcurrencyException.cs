using System;

namespace Cosmos.Data
{
    /// <summary>
    /// Concurrency exception
    /// </summary>
    public class ConcurrencyException : CosmosException
    {
        // ReSharper disable once InconsistentNaming
        private const string FLAG = "__COSMOS_CONCURRENCY";

        // ReSharper disable once InconsistentNaming
        private const string ERROR_MESSAGE = "Concurrency Exception";

        // ReSharper disable once InconsistentNaming
        private const long ERROR_CODE = 7001;

        // ReSharper disable once InconsistentNaming
        private const long EXTEND_ERROR_CODE = 7002;

        /// <summary>
        /// Create a new instance of <see cref="ConcurrencyException"/>
        /// </summary>
        public ConcurrencyException() : this(ERROR_CODE, ERROR_MESSAGE, FLAG) { }

        /// <summary>
        /// Create a new instance of <see cref="ConcurrencyException"/>
        /// </summary>
        /// <param name="exception"></param>
        public ConcurrencyException(Exception exception) : this(ERROR_CODE, ERROR_MESSAGE, FLAG, exception) { }

        /// <summary>
        /// Create a new instance of <see cref="ConcurrencyException"/>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public ConcurrencyException(string message, Exception exception = null) : this(ERROR_CODE, message, FLAG, exception) { }

        /// <summary>
        /// Create a new instance of <see cref="ConcurrencyException"/>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="flag"></param>
        /// <param name="exception"></param>
        public ConcurrencyException(string message, string flag, Exception exception = null) : this(ERROR_CODE, message, flag, exception) { }

        /// <summary>
        /// Create a new instance of <see cref="ConcurrencyException"/>
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public ConcurrencyException(long errorCode, string message, Exception exception = null) : this(errorCode, message, FLAG, exception) { }

        /// <summary>
        /// Create a new instance of <see cref="ConcurrencyException"/>
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="message"></param>
        /// <param name="flag"></param>
        /// <param name="exception"></param>
        public ConcurrencyException(long errorCode, string message, string flag, Exception exception = null) : base(errorCode, message, flag, exception) { }
    }
}