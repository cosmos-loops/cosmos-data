namespace Cosmos.Data.Statements
{
    // ReSharper disable once InconsistentNaming
    public interface ISQLExistsPredicate : ISQLPredicate
    {
        ISQLPredicate Predicate { get; set; }

        bool Not { get; set; }
    }
}