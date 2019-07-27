namespace Cosmos.Data.Statements
{
    // ReSharper disable once InconsistentNaming
    public interface ISQLComparePredicate : ISQLBasePredicate
    {
        SQLOperator Operator { get; set; }

        bool Not { get; set; }
    }
}
