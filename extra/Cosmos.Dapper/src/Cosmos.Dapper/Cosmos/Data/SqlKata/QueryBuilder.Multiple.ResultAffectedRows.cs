namespace Cosmos.Data.SqlKata
{
    public class ResultAffectedRows : IResultAffectedRows
    {
        public int AffectedRows { get; set; }
        public ResultType ResultType { get; set; }
    }
}