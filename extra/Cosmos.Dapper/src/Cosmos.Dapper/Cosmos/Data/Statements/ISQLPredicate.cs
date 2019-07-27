using System.Collections.Generic;
using Cosmos.Data.SqlKata;

namespace Cosmos.Data.Statements
{
    // ReSharper disable once InconsistentNaming
    public interface ISQLPredicate
    {
        string GetSql(ISQLGenerator sqlGenerator, IDictionary<string, object> parameters);
    }
}
