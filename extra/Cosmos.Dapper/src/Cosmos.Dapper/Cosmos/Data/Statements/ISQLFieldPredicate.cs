namespace Cosmos.Data.Statements
{
    // ReSharper disable once InconsistentNaming
    public interface ISQLFieldPredicate : ISQLComparePredicate
    {
        object Value { get; set; }
    }
}
