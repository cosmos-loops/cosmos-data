namespace Cosmos.Data.SqlKata
{
    public class ResultItems : IResultItems
    {
        public object Value { get; set; }
        public ResultType ResultType { get; set; }

        public T GetValue<T>()
        {
            return (T) Value;
        }
    }
}