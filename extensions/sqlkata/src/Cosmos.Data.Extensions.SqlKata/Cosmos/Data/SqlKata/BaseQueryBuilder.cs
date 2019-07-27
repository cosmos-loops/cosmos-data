using System;
using System.Collections.Generic;
using System.Data;
using SqlKata;
using SqlKata.Compilers;

namespace Cosmos.Data.SqlKata
{
    public abstract class BaseQueryBuilder : Query
    {
        protected IDbConnection _connection;
        protected Compiler _compiler;

        protected BaseQueryBuilder(IDbConnection connector, Compiler compiler)
        {
            _connection = connector ?? throw new ArgumentNullException(nameof(connector));
            _compiler = compiler ?? throw new ArgumentNullException(nameof(compiler));
        }

        protected BaseQueryBuilder(IDbConnection connector, Compiler compiler, string table) : base(table)
        {
            _connection = connector ?? throw new ArgumentNullException(nameof(connector));
            _compiler = compiler ?? throw new ArgumentNullException(nameof(compiler));
        }

        protected SqlResult Compiler() => _compiler.Compile(this);

        protected SqlResult Compiler(IEnumerable<Query> queries) => _compiler.Compile(queries);

        protected SqlResult Compiler(params Query[] queryParams) => _compiler.Compile(queryParams);
    }
}