namespace Cosmos.Data.SqlKata
{
    public interface IResultAffectedRows : IResultBase
    {
        int AffectedRows { get; set; }
    }
}