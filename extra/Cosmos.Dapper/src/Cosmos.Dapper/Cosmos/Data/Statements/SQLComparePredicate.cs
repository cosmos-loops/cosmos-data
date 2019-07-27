namespace Cosmos.Data.Statements
{
    // ReSharper disable once InconsistentNaming
    public abstract class SQLComparePredicate : SQLBasePredicate, ISQLComparePredicate
    {
        public SQLOperator Operator { get; set; }

        public bool Not { get; set; }

        public virtual string GetOperatorString()
        {
            switch (Operator)
            {
                case SQLOperator.GT: return Not ? "<=" : ">";
                case SQLOperator.GE: return Not ? "<" : ">=";
                case SQLOperator.LT: return Not ? ">=" : "<";
                case SQLOperator.LE: return Not ? ">" : "<=";
                case SQLOperator.LIKE: return Not ? "NOT LIKE" : "LIKE";
                default: return Not ? "<>" : "=";
            }
        }
    }
}
