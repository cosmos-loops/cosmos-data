namespace Cosmos.Data.Statements
{
    // ReSharper disable once InconsistentNaming
    public interface ISQLPropertyPredicate : ISQLComparePredicate
    {
        string PropertyName2 { get; set; }
    }
}
