namespace Cosmos.Data.Statements
{
    // ReSharper disable once InconsistentNaming
    public interface ISQLBetweenPredicate : ISQLPredicate
    {
        string PropertyName { get; set; }
        SQLBetweenValues Value { get; set; }
        bool Not { get; set; }
    }
}
