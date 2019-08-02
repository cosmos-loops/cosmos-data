using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
using Cosmos;

namespace System.Data
{
    /// <summary>
    /// Extensions for <see cref="DataRow"/>
    /// </summary>
    public static class DataRowExtensions
    {
        /// <summary>
        /// To entity
        /// </summary>
        /// <param name="this"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ToEntity<T>(this DataRow @this) where T : new()
        {
            var type = typeof(T);
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);

            var entity = new T();

            foreach (var property in properties)
            {
                if (@this.Table.Columns.Contains(property.Name))
                {
                    var valueType = property.PropertyType;
                    property.SetValue(entity, @this[property.Name].CastTo(valueType), null);
                }
            }

            foreach (var field in fields)
            {
                if (@this.Table.Columns.Contains(field.Name))
                {
                    var valueType = field.FieldType;
                    field.SetValue(entity, @this[field.Name].CastTo(valueType));
                }
            }

            return entity;
        }

        /// <summary>
        /// To expando object
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static dynamic ToExpandoObject(this DataRow @this)
        {
            dynamic entity = new ExpandoObject();
            var expandoDict = (IDictionary<string, object>) entity;

            foreach (DataColumn column in @this.Table.Columns)
            {
                expandoDict.Add(column.ColumnName, @this[column]);
            }

            return expandoDict;
        }
    }
}