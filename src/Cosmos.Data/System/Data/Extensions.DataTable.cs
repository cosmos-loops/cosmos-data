using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
using Cosmos;

namespace System.Data
{
    /// <summary>
    /// Extensions for <see cref="DataTable"/>
    /// </summary>
    public static class DataTableExtensions
    {
        /// <summary>
        /// Gets first row
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DataRow FirstRow(this DataTable @this) => @this.Rows[0];

        /// <summary>
        /// Gets last row
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DataRow LastRow(this DataTable @this) => @this.Rows[@this.Rows.Count - 1];

        /// <summary>
        /// To a set of entity
        /// </summary>
        /// <param name="this"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> ToEntities<T>(this DataTable @this) where T : new()
        {
            var type = typeof(T);
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);

            var list = new List<T>();

            foreach (DataRow dr in @this.Rows)
            {
                var entity = new T();

                foreach (var property in properties)
                {
                    if (@this.Columns.Contains(property.Name))
                    {
                        var valueType = property.PropertyType;
                        property.SetValue(entity, dr[property.Name].CastTo(valueType), null);
                    }
                }

                foreach (var field in fields)
                {
                    if (@this.Columns.Contains(field.Name))
                    {
                        var valueType = field.FieldType;
                        field.SetValue(entity, dr[field.Name].CastTo(valueType));
                    }
                }

                list.Add(entity);
            }

            return list;
        }

        /// <summary>
        /// To a set of expando object
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static IEnumerable<dynamic> ToExpandoObjects(this DataTable @this)
        {
            var list = new List<dynamic>();

            foreach (DataRow dr in @this.Rows)
            {
                dynamic entity = new ExpandoObject();
                var expandoDict = (IDictionary<string, object>) entity;

                foreach (DataColumn column in @this.Columns)
                {
                    expandoDict.Add(column.ColumnName, dr[column]);
                }


                list.Add(entity);
            }

            return list;
        }
    }
}