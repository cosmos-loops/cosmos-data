namespace Cosmos.Data.SqlKata
{
    public interface IResultItems : IResultBase
    {
        object Value { get; set; }
        T GetValue<T>();
    }
}