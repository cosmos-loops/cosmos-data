using System;
using System.Data;
using Cosmos.Date;
using NodaTime;

namespace Cosmos.Data.Common
{
    /// <summary>
    /// Transaction notify args
    /// </summary>
    public abstract class TransactionNotifyArgs : EventArgs
    {
        /// <summary>
        /// Create a new instance of <see cref="TransactionNotifyArgs"/>.
        /// </summary>
        /// <param name="message"></param>
        public TransactionNotifyArgs(string message)
        {
            Message = message;
        }

        /// <summary>
        /// Gets or sets message
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Gets or sets isolation level
        /// </summary>
        public IsolationLevel? IsolationLevel { get; set; }

        /// <summary>
        /// Gets transaction Id
        /// </summary>
        public abstract string Id { get; }
    }

    /// <summary>
    /// Before transaction notify args
    /// </summary>
    public class BeforeTransNotifyArgs : TransactionNotifyArgs
    {
        /// <inheritdoc />
        public BeforeTransNotifyArgs(string id, string message) : base(message)
        {
            StartTime = DateTimeFactory.Now();
            Id = id;
        }

        /// <summary>
        /// Gets the time of start<br />
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; }

        /// <inheritdoc />
        public override string Id { get; }
    }

    /// <summary>
    /// After transaction notify args
    /// </summary>
    public class AfterTransNotifyArgs : TransactionNotifyArgs
    {
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly BeforeTransNotifyArgs _beforeTransNotifyArgs;

        /// <inheritdoc />
        public AfterTransNotifyArgs(BeforeTransNotifyArgs args, string message, Exception exception = null) : base(message)
        {
            _beforeTransNotifyArgs = args;
            FinTime = DateTimeFactory.Now();
            Duration = _beforeTransNotifyArgs is null
                ? Duration.Zero
                : (FinTime - _beforeTransNotifyArgs.StartTime).AsDuration();
            Exception = exception;
        }

        /// <summary>
        /// Gets or sets exception
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// Gets the time of finish<br />
        /// 完成时间
        /// </summary>
        public DateTime FinTime { get; }

        /// <summary>
        /// Duration<br />
        /// 间隔时间
        /// </summary>
        public Duration Duration { get; }

        /// <inheritdoc />
        public override string Id => _beforeTransNotifyArgs?.Id;
    }
}