namespace System.Data
{
    public static class DataColumnCollectionExtensions
    {
        public static void AddRange(this DataColumnCollection @this, params string[] columns)
        {
            foreach (var column in columns)
            {
                @this.Add(column);
            }
        }
    }
}