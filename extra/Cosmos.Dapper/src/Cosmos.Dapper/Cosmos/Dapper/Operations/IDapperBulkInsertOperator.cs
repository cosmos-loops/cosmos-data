using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cosmos.Dapper.Operations
{
    public interface IDapperBulkInsertOperator
    {
        void Process<T>(IList<T> dataSet) where T : class;
        Task ProcessAsync<T>(IList<T> dataSet) where T : class;
    }
}