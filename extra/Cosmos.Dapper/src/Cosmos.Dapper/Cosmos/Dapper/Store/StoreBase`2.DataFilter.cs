using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cosmos.Dapper.Core.DataFiltering;
using Cosmos.Data.Statements;

namespace Cosmos.Dapper.Store
{
    // ReSharper disable once UnusedTypeParameter
    public abstract partial class StoreBase<TContext, TEntity>
    {

        #region Repo level

        private bool HasResolvedRepoLevelDataFilter { get; set; }
        private ISQLPredicate RepoLevelDataFilterCache { get; set; }
        protected virtual Expression<Func<TEntity, bool>> FilteringExpression { get; set; } = null;

        protected abstract ISQLPredicate CreateRepoLevelDataFilterCache();

        #endregion

        #region Global level

        private ISQLPredicate GlobalLevelDataFilterCache { get; } = GlobalDataFilterManager.GetFilter((typeof(TEntity), typeof(TEntity)));

        #endregion

        protected ISQLPredicate[] RepoLevelDataFilters
        {
            get
            {
                if (!HasResolvedRepoLevelDataFilter)
                {
                    RepoLevelDataFilterCache = CreateRepoLevelDataFilterCache();
                    HasResolvedRepoLevelDataFilter = true;
                }

                return new[] {GlobalLevelDataFilterCache, RepoLevelDataFilterCache};
            }
        }

        #region SqlKata DataFilter

        private string SqlKataWhereRawCache { get; set; } = string.Empty;

        private bool HasResolvedSqlKataDataFilter { get; set; }

        protected string SqlKataRepoLevelDataFilters
        {
            get
            {
                if (!HasResolvedSqlKataDataFilter)
                {
                    SqlKataWhereRawCache = SQLPredicateMerger.Merge(RepoLevelDataFilters).GetSql(RawTypedContext.SqlGenerator, new Dictionary<string, object>());
                    HasResolvedSqlKataDataFilter = true;
                }

                return SqlKataWhereRawCache;
            }
        }

        #endregion
    }
}