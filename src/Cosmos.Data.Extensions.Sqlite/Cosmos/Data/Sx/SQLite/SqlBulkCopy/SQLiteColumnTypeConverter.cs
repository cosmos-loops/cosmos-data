using System;

namespace Cosmos.Data.Sx.SQLite.SqlBulkCopy
{
    /// <summary>
    /// Sqlite column type converter
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public static class SQLiteColumnTypeConverter
    {
        private static Type Int32 => TypeClass.Int32Class;
        private static Type Int32M => TypeClass.Int32NullableClass;
        private static Type Int64 => TypeClass.Int64Class;
        private static Type Int64M => TypeClass.Int64NullableClass;
        private static Type Float32 => TypeClass.FloatClass;
        private static Type Float32M => TypeClass.FloatNullableClass;
        private static Type Float64 => TypeClass.DoubleClass;
        private static Type Float64M => TypeClass.DoubleNullableClass;
        private static Type Numeric => TypeClass.DecimalClass;
        private static Type NumericM => TypeClass.DecimalNullableClass;
        private static Type Bool => TypeClass.BooleanClass;
        private static Type BoolM => TypeClass.BooleanNullableClass;
        private static Type Char => TypeClass.CharClass;
        private static Type CharM => TypeClass.CharNullableClass;
        private static Type Text => TypeClass.StringClass;
        private static Type Date => TypeClass.DateTimeClass;
        private static Type DateM => TypeClass.DateTimeNullableClass;
        private static Type Binary => TypeClass.ByteArrayClass;

        /// <summary>
        /// To Sqlite column type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static SQLiteColumnType ToSqliteColumnType(Type type)
        {
            if (type == Int32 || type == Int32M || type == Int64 || type == Int64M)
                return SQLiteColumnType.Integer;

            if (type == Float32 || type == Float32M || type == Float64 || type == Float64M)
                return SQLiteColumnType.Real;

            if (type == Numeric || type == NumericM)
                return SQLiteColumnType.Numeric;

            if (type == Bool || type == BoolM)
                return SQLiteColumnType.Boolean;

            if (type == Char || type == CharM || type == Text)
                return SQLiteColumnType.Text;

            if (type == Date || type == DateM)
                return SQLiteColumnType.Date;

            if (type == Binary)
                return SQLiteColumnType.Blob;

            return SQLiteColumnType.Text;
        }

        /// <summary>
        /// To Sqlite column type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static SQLiteColumnType ToSqliteColumnType(string type)
        {
            var columnType = type.ToUpper();
            var columnSqliteType = new SQLiteColumnType();

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
                    columnSqliteType = SQLiteColumnType.Integer;
                    break;
                case "CLOB":
                case "TEXT":
                    columnSqliteType = SQLiteColumnType.Text;
                    break;
                case "BLOB":
                    columnSqliteType = SQLiteColumnType.Blob;
                    break;
                case "REAL":
                case "DOUBLE":
                case "DOUBLE PRECISION":
                case "FLOAT":
                    columnSqliteType = SQLiteColumnType.Real;
                    break;
                case "NUMERIC":
                    columnSqliteType = SQLiteColumnType.Numeric;
                    break;
                case "BOOLEAN":
                    columnSqliteType = SQLiteColumnType.Boolean;
                    break;
                case "DATE":
                case "DATETIME":
                    columnSqliteType = SQLiteColumnType.Date;
                    break;
                default: // look for fringe cases that need logic
                    if (columnType.StartsWith("CHARACTER"))
                        columnSqliteType = SQLiteColumnType.Text;
                    if (columnType.StartsWith("VARCHAR"))
                        columnSqliteType = SQLiteColumnType.Text;
                    if (columnType.StartsWith("VARYING CHARACTER"))
                        columnSqliteType = SQLiteColumnType.Text;
                    if (columnType.StartsWith("NCHAR"))
                        columnSqliteType = SQLiteColumnType.Text;
                    if (columnType.StartsWith("NATIVE CHARACTER"))
                        columnSqliteType = SQLiteColumnType.Text;
                    if (columnType.StartsWith("NVARCHAR"))
                        columnSqliteType = SQLiteColumnType.Text;
                    if (columnType.StartsWith("NVARCHAR"))
                        columnSqliteType = SQLiteColumnType.Text;
                    if (columnType.StartsWith("DECIMAL"))
                        columnSqliteType = SQLiteColumnType.Numeric;
                    break;
            }

            return columnSqliteType;
        }
    }
}