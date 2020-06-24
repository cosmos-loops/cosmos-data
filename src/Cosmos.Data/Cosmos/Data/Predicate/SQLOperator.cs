using System.Linq.Expressions;

namespace Cosmos.Data.Predicate
{
    /// <summary>
    /// Predicate operator
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public enum SQLOperator
    {
        /// <summary>
        /// Equal, same as SQLOperatorSlim.EQ
        /// </summary>
        Equal = ExpressionType.Equal,

        /// <summary>
        /// Not equal
        /// </summary>
        NotEqual = ExpressionType.NotEqual,

        /// <summary>
        /// Greater than or equal
        /// </summary>
        GreaterThanOrEqual = ExpressionType.GreaterThanOrEqual,

        /// <summary>
        /// Less than or equal
        /// </summary>
        LessThanOrEqual = ExpressionType.LessThanOrEqual,

        /// <summary>
        /// Greater than
        /// </summary>
        GreaterThan = ExpressionType.GreaterThan,

        /// <summary>
        /// Less than
        /// </summary>
        LessThan = ExpressionType.LessThan,

        /// <summary>
        /// Contains, can only be used fot String
        /// </summary>
        Contains = 1000,

        /// <summary>
        /// End with, can only be used fot String
        /// </summary>
        EndWith,

        /// <summary>
        /// Start with, can only be used fot String
        /// </summary>
        StartsWith
    }
}