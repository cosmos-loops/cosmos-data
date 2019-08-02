namespace Cosmos.Data.Internals
{
    /// <summary>
    /// Data support flags
    /// </summary>
    public static class DataSupportFlag
    {
        private static bool InternalValue { get; set; }

        /// <summary>
        /// Gets or sets value for Cosmos.Data
        /// </summary>
        public static bool Value
        {
            get => InternalValue;
            set => InternalValue = value;
        }
    }
}