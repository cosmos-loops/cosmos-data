using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using Cosmos;

namespace System.Data
{
    /// <summary>
    /// Extensions for <see cref="IDataReader"/>
    /// </summary>
    public static class DataReaderExtensions
    {
        /// <summary>
        /// Is DbNull
        /// </summary>
        /// <param name="this"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        // ReSharper disable once InconsistentNaming
        public static bool IsDBNull(this IDataReader @this, string name)
            => @this.IsDBNull(@this.GetOrdinal(name));

        /// <summary>
        /// To DataTable
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DataTable ToDataTable(this IDataReader @this)
        {
            var dt = new DataTable();
            dt.Load(@this);
            return dt;
        }

        /// <summary>
        /// To Entity
        /// </summary>
        /// <param name="this"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ToEntity<T>(this IDataReader @this) where T : new()
        {
            var type = typeof(T);
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);

            var entity = new T();

            var hash = new HashSet<string>(Enumerable.Range(0, @this.FieldCount).Select(@this.GetName));

            foreach (var property in properties)
            {
                if (hash.Contains(property.Name))
                {
                    var valueType = property.PropertyType;
                    property.SetValue(entity, @this[property.Name].CastTo(valueType), null);
                }
            }

            foreach (var field in fields)
            {
                if (hash.Contains(field.Name))
                {
                    var valueType = field.FieldType;
                    field.SetValue(entity, @this[field.Name].CastTo(valueType));
                }
            }

            return entity;
        }

        /// <summary>
        /// To a set of entity
        /// </summary>
        /// <param name="this"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> ToEntities<T>(this IDataReader @this) where T : new()
        {
            var type = typeof(T);
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);

            var list = new List<T>();

            var hash = new HashSet<string>(Enumerable.Range(0, @this.FieldCount).Select(@this.GetName));

            while (@this.Read())
            {
                var entity = new T();

                foreach (var property in properties)
                {
                    if (hash.Contains(property.Name))
                    {
                        var valueType = property.PropertyType;
                        property.SetValue(entity, @this[property.Name].CastTo(valueType), null);
                    }
                }

                foreach (var field in fields)
                {
                    if (hash.Contains(field.Name))
                    {
                        var valueType = field.FieldType;
                        field.SetValue(entity, @this[field.Name].CastTo(valueType));
                    }
                }

                list.Add(entity);
            }

            return list;
        }

        /// <summary>
        /// To expando object
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static dynamic ToExpandoObject(this IDataReader @this)
        {
            var columnNames = Enumerable.Range(0, @this.FieldCount)
                .Select(x => new KeyValuePair<int, string>(x, @this.GetName(x)))
                .ToDictionary(pair => pair.Key);

            dynamic entity = new ExpandoObject();
            var expandoDict = (IDictionary<string, object>) entity;

            Enumerable.Range(0, @this.FieldCount)
                .ToList()
                .ForEach(x => expandoDict.Add(columnNames[x].Value, @this[x]));

            return entity;
        }

        /// <summary>
        /// To a set of expando object
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static IEnumerable<dynamic> ToExpandoObjects(this IDataReader @this)
        {
            var columnNames = Enumerable.Range(0, @this.FieldCount)
                .Select(x => new KeyValuePair<int, string>(x, @this.GetName(x)))
                .ToDictionary(pair => pair.Key);

            var list = new List<dynamic>();

            while (@this.Read())
            {
                dynamic entity = new ExpandoObject();
                var expandoDict = (IDictionary<string, object>) entity;

                Enumerable.Range(0, @this.FieldCount)
                    .ToList()
                    .ForEach(x => expandoDict.Add(columnNames[x].Value, @this[x]));

                list.Add(entity);
            }

            return list;
        }

        /// <summary>
        /// Gets column name
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetColumnNames(this IDataRecord @this)
            => Enumerable.Range(0, @this.FieldCount).Select(@this.GetName).ToList();

        /// <summary>
        /// Gets value as...
        /// </summary>
        /// <param name="this"></param>
        /// <param name="index"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetValueAs<T>(this IDataReader @this, int index)
            => (T) @this.GetValue(index);

        /// <summary>
        /// Gets value as...
        /// </summary>
        /// <param name="this"></param>
        /// <param name="columnName"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetValueAs<T>(this IDataReader @this, string columnName)
            => (T) @this.GetValue(@this.GetOrdinal(columnName));

        /// <summary>
        /// Gets value as... or default...
        /// </summary>
        /// <param name="this"></param>
        /// <param name="index"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetValueAsOrDefault<T>(this IDataReader @this, int index)
        {
            try
            {
                return (T) @this.GetValue(index);
            }
            catch
            {
                return default;
            }
        }

        /// <summary>
        /// Gets value as... or default...
        /// </summary>
        /// <param name="this"></param>
        /// <param name="index"></param>
        /// <param name="defaultValue"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetValueAsOrDefault<T>(this IDataReader @this, int index, T defaultValue)
        {
            try
            {
                return (T) @this.GetValue(index);
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Gets value as... or default...
        /// </summary>
        /// <param name="this"></param>
        /// <param name="index"></param>
        /// <param name="defaultValueFactory"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetValueAsOrDefault<T>(this IDataReader @this, int index, Func<IDataReader, int, T> defaultValueFactory)
        {
            try
            {
                return (T) @this.GetValue(index);
            }
            catch
            {
                return defaultValueFactory(@this, index);
            }
        }

        /// <summary>
        /// Gets value as... or default...
        /// </summary>
        /// <param name="this"></param>
        /// <param name="columnName"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetValueAsOrDefault<T>(this IDataReader @this, string columnName)
        {
            try
            {
                return (T) @this.GetValue(@this.GetOrdinal(columnName));
            }
            catch
            {
                return default;
            }
        }

        /// <summary>
        /// Gets value as... or default value
        /// </summary>
        /// <param name="this"></param>
        /// <param name="columnName"></param>
        /// <param name="defaultValue"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetValueAsOrDefault<T>(this IDataReader @this, string columnName, T defaultValue)
        {
            try
            {
                return (T) @this.GetValue(@this.GetOrdinal(columnName));
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Gets value as... or default value
        /// </summary>
        /// <param name="this"></param>
        /// <param name="columnName"></param>
        /// <param name="defaultValueFactory"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetValueAsOrDefault<T>(this IDataReader @this, string columnName, Func<IDataReader, string, T> defaultValueFactory)
        {
            try
            {
                return (T) @this.GetValue(@this.GetOrdinal(columnName));
            }
            catch
            {
                return defaultValueFactory(@this, columnName);
            }
        }

        /// <summary>
        /// Gets value to...
        /// </summary>
        /// <param name="this"></param>
        /// <param name="index"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetValueTo<T>(this IDataReader @this, int index)
            => @this.GetValue(index).CastTo<T>();

        /// <summary>
        /// Gets value to...
        /// </summary>
        /// <param name="this"></param>
        /// <param name="columnName"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetValueTo<T>(this IDataReader @this, string columnName)
            => @this.GetValue(@this.GetOrdinal(columnName)).CastTo<T>();

        /// <summary>
        /// Gets value to...or default...
        /// </summary>
        /// <param name="this"></param>
        /// <param name="index"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetValueToOrDefault<T>(this IDataReader @this, int index)
        {
            try
            {
                return @this.GetValue(index).CastTo<T>();
            }
            catch
            {
                return default;
            }
        }

        /// <summary>
        /// Gets value to...or default vaule
        /// </summary>
        /// <param name="this"></param>
        /// <param name="index"></param>
        /// <param name="defaultValue"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetValueToOrDefault<T>(this IDataReader @this, int index, T defaultValue)
        {
            try
            {
                return @this.GetValue(index).CastTo<T>();
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Gets value to...or default value
        /// </summary>
        /// <param name="this"></param>
        /// <param name="index"></param>
        /// <param name="defaultValueFactory"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetValueToOrDefault<T>(this IDataReader @this, int index, Func<IDataReader, int, T> defaultValueFactory)
        {
            try
            {
                return @this.GetValue(index).CastTo<T>();
            }
            catch
            {
                return defaultValueFactory(@this, index);
            }
        }

        /// <summary>
        /// Gets value to...or default...
        /// </summary>
        /// <param name="this"></param>
        /// <param name="columnName"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetValueToOrDefault<T>(this IDataReader @this, string columnName)
        {
            try
            {
                return @this.GetValue(@this.GetOrdinal(columnName)).CastTo<T>();
            }
            catch
            {
                return default;
            }
        }

        /// <summary>
        /// Gets value to...or default value
        /// </summary>
        /// <param name="this"></param>
        /// <param name="columnName"></param>
        /// <param name="defaultValue"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetValueToOrDefault<T>(this IDataReader @this, string columnName, T defaultValue)
        {
            try
            {
                return @this.GetValue(@this.GetOrdinal(columnName)).CastTo<T>();
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Gets value to...or default value
        /// </summary>
        /// <param name="this"></param>
        /// <param name="columnName"></param>
        /// <param name="defaultValueFactory"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetValueToOrDefault<T>(this IDataReader @this, string columnName, Func<IDataReader, string, T> defaultValueFactory)
        {
            try
            {
                return @this.GetValue(@this.GetOrdinal(columnName)).CastTo<T>();
            }
            catch
            {
                return defaultValueFactory(@this, columnName);
            }
        }

        /// <summary>
        /// For each
        /// </summary>
        /// <param name="this"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IDataReader ForEach(this IDataReader @this, Action<IDataReader> action)
        {
            while (@this.Read())
            {
                action(@this);
            }

            return @this;
        }

        /// <summary>
        /// Contains column
        /// </summary>
        /// <param name="this"></param>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        public static bool ContainsColumn(this IDataReader @this, int columnIndex)
        {
            try
            {
                // Check if FieldCount is implemented first
                return @this.FieldCount > columnIndex;
            }
            catch (Exception)
            {
                try
                {
                    return @this[columnIndex] != null;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Contains column
        /// </summary>
        /// <param name="this"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static bool ContainsColumn(this IDataReader @this, string columnName)
        {
            try
            {
                // Check if GetOrdinal is implemented first
                return @this.GetOrdinal(columnName) != -1;
            }
            catch (Exception)
            {
                try
                {
                    return @this[columnName] != null;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}