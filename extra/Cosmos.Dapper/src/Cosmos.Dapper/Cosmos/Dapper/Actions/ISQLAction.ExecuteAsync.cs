using System.Threading;
using System.Threading.Tasks;

namespace Cosmos.Dapper.Actions
{
    // ReSharper disable once InconsistentNaming
    internal interface IAsynchronousExecutableSQLAction
    {
        Task ExecuteCalledFromBankAsync(CancellationToken cancellationToken);
    }
}