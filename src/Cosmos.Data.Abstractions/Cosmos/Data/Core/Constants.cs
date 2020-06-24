namespace Cosmos.Data.Core
{
    /// <summary>
    /// Data constants
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// DbContext initialize actions clear
        /// </summary>
        public const string DbxClearTaskName = "_dbContextInitializeActionsClear";

        /// <summary>
        /// Scoped repository clear task
        /// </summary>
        public const string ScopedRepoClearTaskName = "_instanceClear";

        /// <summary>
        /// Transaction wrapper clear task
        /// </summary>
        public const string TransWpClearTaskName = "_transactionWrapper";
    }
}