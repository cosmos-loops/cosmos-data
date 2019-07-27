using System;
using SqlKata;

namespace Cosmos.Data.SqlKata
{
    public interface IQueryBuilderMultipleDeleteDapper : IQueryBuilderMultipleExecuteDapper<IResultAffectedRows>
    {
        IQueryBuilderMultipleDeleteDapper AddDelete(Query query);
        IQueryBuilderMultipleDeleteDapper AddDelete(Func<Query, Query> query);
    }
}