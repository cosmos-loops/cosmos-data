using System;
using SqlKata;

namespace Cosmos.Data.SqlKata
{
    public interface IQueryBuilderMultipleSelectDapper : IQueryBuilderMultipleExecuteDapper<IResultItems>
    {
        IQueryBuilderMultipleSelectDapper AddSelect<TReturn>(Query query);
        IQueryBuilderMultipleSelectDapper AddSelect<TReturn>(Func<Query, Query> query);
    }
}