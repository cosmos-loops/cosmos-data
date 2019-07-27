namespace System.Data.SQLite.SqlBulkCopy
{
    // ReSharper disable once InconsistentNaming
    public static class SQLiteColumnTypeConverter
    {
        private static Type Int32 => typeof(int);
        private static Type Int32M => typeof(int?);
        private static Type Int64 => typeof(long);
        private static Type Int64M => typeof(long?);
        private static Type Float32 => typeof(float);
        private static Type Float32M => typeof(float?);
        private static Type Float64 => typeof(double);
        private static Type Float64M => typeof(double?);
        private static Type Numeric => typeof(decimal);
        private static Type NumericM => typeof(decimal?);
        private static Type Bool => typeof(bool);
        private static Type BoolM => typeof(bool?);
        private static Type Char => typeof(char);
        private static Type CharM => typeof(char?);
        private static Type Text => typeof(string);
        private static Type Date => typeof(DateTime);
        private static Type DateM => typeof(DateTime?);
        private static Type Binary => typeof(byte[]);
        
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