using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Reflection;
using Cosmos.Conversions;

namespace Cosmos.Data.Sx
{
    /// <summary>
    /// Extensions for <see cref="DataTable"/>
    /// </summary>
    public static class DataTableExtensions
    {
        /// <summary>
        /// Gets first row
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataRow FirstRow(this DataTable dt)
        {
            return dt.Rows[0];
        }

        /// <summary>
        /// Gets last row
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataRow LastRow(this DataTable dt)
        {
#if NETFRAMEWORK || NETSTANDARD2_0
            return dt.Rows[dt.Rows.Count - 1];
#else
            return dt.Rows[^1];
#endif
        }

        /// <summary>
        /// To a set of entity
        /// </summary>
        /// <param name="dt"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> ToEntities<T>(this DataTable dt) where T : new()
        {
            dt.CheckNull(nameof(dt));
          
            var type = typeof(T);
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);

            var list = new List<T>();

            foreach (DataRow dr in dt.Rows)
            {
                var entity = new T();

                foreach (var property in properties)
                {
                    if (dt.Columns.Contains(property.Name))
                    {
                        var valueType = property.PropertyType;
                        property.SetValue(entity, dr[property.Name].CastTo(valueType), null);
                    }
                }

                foreach (var field in fields)
                {
                    if (dt.Columns.Contains(field.Name))
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
        /// <param name="dt"></param>
        /// <returns></returns>
        public static IEnumerable<dynamic> ToExpandoObjects(this DataTable dt)
        {
            dt.CheckNull(nameof(dt));
           
            var list = new List<dynamic>();

            foreach (DataRow dr in dt.Rows)
            {
                dynamic entity = new ExpandoObject();
                var expandoDict = (IDictionary<string, object>) entity;

                foreach (DataColumn column in dt.Columns)
                {
                    expandoDict.Add(column.ColumnName, dr[column]);
                }

                list.Add(entity);
            }

            return list;
        }
    }
}