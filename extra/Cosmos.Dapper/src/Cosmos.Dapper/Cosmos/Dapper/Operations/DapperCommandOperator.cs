﻿using System;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Dapper.Core;
using Cosmos.Data.Transaction;
using Dapper;

namespace Cosmos.Dapper.Operations
{
    public partial class DapperCommandOperator : IDapperCommandOperator
    {
        private readonly IDapperConnector _connector;
        private readonly ITransactionWrapper _transactionPointer;
        private readonly IDapperMappingConfig _mappingConfig;
        private Func<CommandDefinition, CommandDefinition> InjectTransaction { get; set; }

        internal DapperCommandOperator(IDapperConnector connector, IDapperMappingConfig config, Func<CommandDefinition, CommandDefinition> injectTransaction)
        {
            _connector = connector ?? throw new ArgumentNullException(nameof(connector));
            _mappingConfig = config ?? throw new ArgumentNullException(nameof(config));
            _transactionPointer = connector.TransactionWrapper;
            InjectTransaction = injectTransaction;
        }

        private IDbConnection Connection => _connector.Connection;

        private IDbTransaction Transaction => _transactionPointer.CurrentTransaction;

        private DapperOptions Options => _mappingConfig.Options;

        private void PrepareConnectionAndTransaction()
        {
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }
        }

        private async Task PrepareConnectionAndTransactionAsync(CancellationToken cancellationToken)
        {
            if (Connection.State != ConnectionState.Open)
            {
                await This().OpenAsync(cancellationToken).ConfigureAwait(false);
            }

            DbConnection This() => Connection as DbConnection;
        }
    }
}