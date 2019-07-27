using SqlKata.Compilers;

namespace Cosmos.Dapper.Core.SqlKata
{
    public interface ISqlKataCompilerCreator
    {
        Compiler Create();
    }
}