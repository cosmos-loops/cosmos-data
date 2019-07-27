namespace Cosmos.Data.Internals
{
    public static class DataSupportFlag
    {
        private static bool InternalValue { get; set; }

        public static bool Value
        {
            get => InternalValue;
            set => InternalValue = value;
        }
    }
}
