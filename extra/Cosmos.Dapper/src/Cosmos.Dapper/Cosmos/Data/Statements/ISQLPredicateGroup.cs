using System.Collections.Generic;

namespace Cosmos.Data.Statements
{
    // ReSharper disable once InconsistentNaming
    public interface ISQLPredicateGroup : ISQLPredicate
    {
        SQLGroupOperator Operator { get; set; }
     
        IList<ISQLPredicate> Predicates { get; set; }
    }
}
