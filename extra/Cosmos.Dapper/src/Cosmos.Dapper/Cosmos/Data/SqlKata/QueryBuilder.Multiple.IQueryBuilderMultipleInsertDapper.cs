using System;
using SqlKata;

namespace Cosmos.Data.SqlKata
{
    public interface IQueryBuilderMultipleInsertDapper : IQueryBuilderMultipleExecuteDapper<IResultItems>
    {
        IQueryBuilderMultipleInsertDapper AddInsert<TReturn>(Query query);
        IQueryBuilderMultipleInsertDapper AddInsert<TReturn>(Func<Query, Query> query);
    }
}