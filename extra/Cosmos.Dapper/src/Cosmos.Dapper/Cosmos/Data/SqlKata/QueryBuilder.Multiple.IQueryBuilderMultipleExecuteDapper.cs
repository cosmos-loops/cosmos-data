using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cosmos.Data.SqlKata
{
    public interface IQueryBuilderMultipleExecuteDapper<TResult>
    {
        IEnumerable<TResult> ExecuteMultiple();
        Task<IEnumerable<TResult>> ExecuteMultipleAsync();
    }
}