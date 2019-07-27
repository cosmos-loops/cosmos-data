using SqlKata.Compilers;

namespace Cosmos.Dapper.Core.SqlKata
{
    public class SqlKataCompilerCreator<TCompiler> : ISqlKataCompilerCreator where TCompiler : Compiler, new()
    {
        public Compiler Create() => new TCompiler();
    }
}