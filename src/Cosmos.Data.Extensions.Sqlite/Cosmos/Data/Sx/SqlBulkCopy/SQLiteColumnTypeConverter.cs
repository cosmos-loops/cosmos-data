using System;
using Cosmos.Reflection;

namespace Cosmos.Data.Sx.SqlBulkCopy
{
    /// <summary>
    /// Sqlite column type converter
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public static class SqliteColumnTypeConverter
    {
        private static Type Int32 => TypeClass.Int32Clazz;
        private static Type Int32M => TypeClass.Int32NullableClazz;
        private static Type Int64 => TypeClass.Int64Clazz;
        private static Type Int64M => TypeClass.Int64NullableClazz;
        private static Type Float32 => TypeClass.FloatClazz;
        private static Type Float32M => TypeClass.FloatNullableClazz;
        private static Type Float64 => TypeClass.DoubleClazz;
        private static Type Float64M => TypeClass.DoubleNullableClazz;
        private static Type Numeric => TypeClass.DecimalClazz;
        private static Type NumericM => TypeClass.DecimalNullableClazz;
        private static Type Bool => TypeClass.BooleanClazz;
        private static Type BoolM => TypeClass.BooleanNullableClazz;
        private static Type Char => TypeClass.CharClazz;
        private static Type CharM => TypeClass.CharNullableClazz;
        private static Type Text => TypeClass.StringClazz;
        private static Type Date => TypeClass.DateTimeClazz;
        private static Type DateM => TypeClass.DateTimeNullableClazz;
        private static Type Binary => TypeClass.ByteArrayClazz;

        /// <summary>
        /// To Sqlite column type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static SqliteColumnType ToSqliteColumnType(Type type)
        {
            if (type == Int32 || type == Int32M || type == Int64 || type == Int64M)
                return SqliteColumnType.Integer;

            if (type == Float32 || type == Float32M || type == Float64 || type == Float64M)
                return SqliteColumnType.Real;

            if (type == Numeric || type == NumericM)
                return SqliteColumnType.Numeric;

            if (type == Bool || type == BoolM)
                return SqliteColumnType.Boolean;

            if (type == Char || type == CharM || type == Text)
                return SqliteColumnType.Text;

            if (type == Date || type == DateM)
                return SqliteColumnType.Date;

            if (type == Binary)
                return SqliteColumnType.Blob;

            return SqliteColumnType.Text;
        }

        /// <summary>
        /// To Sqlite column type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static SqliteColumnType ToSqliteColumnType(string type)
        {
            var columnType = type.ToUpper();
            var columnSqliteType = new SqliteColumnType();

            switch (columnType)
            {
                case "INTEGER":
                case "TINYINT":
                case "SMALLINT":
                case "MEDIUMINT":
                case "BIGINT":
                case "UNSIGNED BIG INT":
                case "INT2":
                case "INT8":
                case "INT":
                    columnSqliteType = SqliteColumnType.Integer;
                    break;
                case "CLOB":
                case "TEXT":
                    columnSqliteType = SqliteColumnType.Text;
                    break;
                case "BLOB":
                    columnSqliteType = SqliteColumnType.Blob;
                    break;
                case "REAL":
                case "DOUBLE":
                case "DOUBLE PRECISION":
                case "FLOAT":
                    columnSqliteType = SqliteColumnType.Real;
                    break;
                case "NUMERIC":
                    columnSqliteType = SqliteColumnType.Numeric;
                    break;
                case "BOOLEAN":
                    columnSqliteType = SqliteColumnType.Boolean;
                    break;
                case "DATE":
                case "DATETIME":
                    columnSqliteType = SqliteColumnType.Date;
                    break;
                default: // look for fringe cases that need logic
                    if (columnType.StartsWith("CHARACTER"))
                        columnSqliteType = SqliteColumnType.Text;
                    if (columnType.StartsWith("VARCHAR"))
                        columnSqliteType = SqliteColumnType.Text;
                    if (columnType.StartsWith("VARYING CHARACTER"))
                        columnSqliteType = SqliteColumnType.Text;
                    if (columnType.StartsWith("NCHAR"))
                        columnSqliteType = SqliteColumnType.Text;
                    if (columnType.StartsWith("NATIVE CHARACTER"))
                        columnSqliteType = SqliteColumnType.Text;
                    if (columnType.StartsWith("NVARCHAR"))
                        columnSqliteType = SqliteColumnType.Text;
                    if (columnType.StartsWith("NVARCHAR"))
                        columnSqliteType = SqliteColumnType.Text;
                    if (columnType.StartsWith("DECIMAL"))
                        columnSqliteType = SqliteColumnType.Numeric;
                    break;
            }

            return columnSqliteType;
        }
    }
}