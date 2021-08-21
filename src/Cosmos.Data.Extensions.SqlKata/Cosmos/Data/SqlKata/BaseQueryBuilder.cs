using System;
using System.Collections.Generic;
using System.Data;
using SqlKata;
using SqlKata.Compilers;

namespace Cosmos.Data.SqlKata
{
    /// <summary>
    /// Base sql-kata query builder
    /// </summary>
    public abstract class BaseQueryBuilder : Query
    {
        /// <summary>
        /// Connection
        /// </summary>
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once NotAccessedField.Global
        protected IDbConnection _connection;

        /// <summary>
        /// Compiler
        /// </summary>
        // ReSharper disable once FieldCanBeMadeReadOnly.Global
        // ReSharper disable once InconsistentNaming
        protected Compiler _compiler;

        /// <summary>
        /// Create a new instance of <see cref="BaseQueryBuilder"/>
        /// </summary>
        /// <param name="connector"></param>
        /// <param name="compiler"></param>
        protected BaseQueryBuilder(IDbConnection connector, Compiler compiler)
        {
            _connection = connector ?? throw new ArgumentNullException(nameof(connector));
            _compiler = compiler ?? throw new ArgumentNullException(nameof(compiler));
        }

        /// <summary>
        /// Create a new instance of <see cref="BaseQueryBuilder"/>
        /// </summary>
        /// <param name="connector"></param>
        /// <param name="compiler"></param>
        /// <param name="table"></param>
        protected BaseQueryBuilder(IDbConnection connector, Compiler compiler, string table) : base(table)
        {
            _connection = connector ?? throw new ArgumentNullException(nameof(connector));
            _compiler = compiler ?? throw new ArgumentNullException(nameof(compiler));
        }

        /// <summary>
        /// Compiler
        /// </summary>
        /// <returns></returns>
        protected SqlResult Compiler() => _compiler.Compile(this);

        /// <summary>
        /// Compiler
        /// </summary>
        /// <param name="queries"></param>
        /// <returns></returns>
        protected SqlResult Compiler(IEnumerable<Query> queries) => _compiler.Compile(queries);

        /// <summary>
        /// Compiler
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        protected SqlResult Compiler(params Query[] queryParams) => _compiler.Compile(queryParams);
    }
}