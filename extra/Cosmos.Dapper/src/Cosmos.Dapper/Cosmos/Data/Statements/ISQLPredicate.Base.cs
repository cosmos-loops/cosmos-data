namespace Cosmos.Data.Statements
{
    // ReSharper disable once InconsistentNaming
    public interface ISQLBasePredicate : ISQLPredicate
    {
        string PropertyName { get; set; }
    }
}
