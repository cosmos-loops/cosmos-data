using System;
using System.Data;
using System.IO;
using System.Text;
using MySql.Data.MySqlClient;

namespace Cosmos.Data.Sx.SqlBulkCopy
{
    internal class MySqlBulkCopyHelper
    {
        public static MySqlBulkLoader GetBulkLoader(MySqlConnection conn, DataTable table, string tableName, int timeout, string secureFilePriv)
        {
            var bulkLoader = new MySqlBulkLoader(conn)
            {
                FieldTerminator = FieldTerminator,
                FieldQuotationCharacter = FieldQuotationCharacter,
                EscapeCharacter = EscapeCharacter,
                LineTerminator = _lineTerminator,
                FileName = DataTableToCsv(table, secureFilePriv),
                NumberOfLinesToSkip = 0,
                TableName = tableName,
                Timeout = timeout
            };

            return bulkLoader;
        }

        private const char EscapeCharacter = '"';
        private const char FieldQuotationCharacter = '"';
        private const string FieldTerminator = ",";

        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private static string _lineTerminator = Environment.NewLine;

        private static string DataTableToCsv(DataTable table, string secureFilePriv)
        {
            var dataBuilder = new StringBuilder();
            foreach (DataRow row in table.Rows)
            {
                var colIndex = 0;
                foreach (DataColumn dataColumn in table.Columns)
                {
                    if (colIndex != 0) dataBuilder.Append(FieldTerminator);

                    if (dataColumn.DataType == CommonType.String
                     && !row.IsNull(dataColumn)
                     && row[dataColumn].ToString().Contains(FieldTerminator))
                    {
                        dataBuilder.AppendFormat("\"{0}\"", row[dataColumn].ToString().Replace("\"", "\"\""));
                    }
                    else
                    {
                        var colValStr = dataColumn.AutoIncrement ? "" : row[dataColumn]?.ToString();
                        dataBuilder.Append(colValStr);
                    }

                    colIndex++;
                }

                dataBuilder.Append(_lineTerminator);
            }

            var currentFilePath = GetFilePath(secureFilePriv);

            ToSaveFile(currentFilePath, dataBuilder);

            return currentFilePath;
        }

        private static string GetFilePath(string fileDir)
        {
            var date = DateTime.Now.ToString("yyyy-MM-dd");
            var fileName = Guid.NewGuid().ToString("N") + ".csv";
            return Path.Combine(fileDir, date, fileName);
        }

        private static void ToSaveFile(string filePath, StringBuilder datBuilder)
        {
            File.WriteAllText(filePath, datBuilder.ToString());
        }

        private static void ToDeleteFile(string filePath)
        {
            File.Delete(filePath);
        }
    }
}