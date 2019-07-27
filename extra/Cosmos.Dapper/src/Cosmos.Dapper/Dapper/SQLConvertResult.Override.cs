using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Threading;

namespace Dapper
{
    public class OverrideSQLConvertResult : SQLConvertResult
    {
        public OverrideSQLConvertResult(SQLConvertResult sql, object objectParameter, bool enableNullParameter = false) : base(sql)
        {
            ObjectParameter = objectParameter;
            EnableNullParameter = enableNullParameter;
        }

        public object ObjectParameter { get; set; }

        public bool EnableNullParameter { get; set; }

        public override CommandDefinition ToSQLCommand(
            IDbTransaction transaction = null,
            int? timeout = null,
            CommandType? commandType = null,
            CommandFlags commandFlags = CommandFlags.Buffered,
            CancellationToken cancellationToken = default)
        {
            if (EnableNullParameter)
                return new CommandDefinition(Sql, ObjectParameter, transaction, timeout, commandType, commandFlags, cancellationToken);
            return ObjectParameter == null
                ? base.ToSQLCommand(transaction, timeout, commandType, commandFlags, cancellationToken)
                : new CommandDefinition(Sql, ObjectParameter, transaction, timeout, commandType, commandFlags, cancellationToken);
        }
    }
}