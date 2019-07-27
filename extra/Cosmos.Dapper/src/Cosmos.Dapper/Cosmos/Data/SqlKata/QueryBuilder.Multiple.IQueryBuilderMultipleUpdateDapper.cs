using System;
using SqlKata;

namespace Cosmos.Data.SqlKata
{
    public interface IQueryBuilderMultipleUpdateDapper : IQueryBuilderMultipleExecuteDapper<IResultAffectedRows>
    {
        IQueryBuilderMultipleUpdateDapper AddUpdate(Query query);
        IQueryBuilderMultipleUpdateDapper AddUpdate(Func<Query, Query> query);
    }
}