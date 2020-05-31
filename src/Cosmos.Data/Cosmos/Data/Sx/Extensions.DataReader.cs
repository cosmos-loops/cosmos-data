using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using Cosmos.Conversions;
using Cosmos.Exceptions;

namespace Cosmos.Data.Sx
{
    /// <summary>
    /// Extensions for <see cref="IDataReader"/>
    /// </summary>
    public static class DataReaderExtensions
    {
        /// <summary>
        /// Is DbNull
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        // ReSharper disable once InconsistentNaming
        public static bool IsDBNull(this IDataReader reader, string name)
        {
            reader.CheckNull(nameof(reader));
            return reader.IsDBNull(reader.GetOrdinal(name));
        }

        /// <summary>
        /// To DataTable
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static DataTable ToDataTable(this IDataReader reader)
        {
            reader.CheckNull(nameof(reader));
            var dt = new DataTable();
            dt.Load(reader);
            return dt;
        }

        /// <summary>
        /// To Entity
        /// </summary>
        /// <param name="reader"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ToEntity<T>(this IDataReader reader) where T : new()
        {
            reader.CheckNull(nameof(reader));

            var type = typeof(T);
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);

            var entity = new T();

            var hash = new HashSet<string>(Enumerable.Range(0, reader.FieldCount).Select(reader.GetName));

            foreach (var property in properties)
            {
                if (hash.Contains(property.Name))
                {
                    var valueType = property.PropertyType;
                    property.SetValue(entity, reader[property.Name].CastTo(valueType), null);
                }
            }

            foreach (var field in fields)
            {
                if (hash.Contains(field.Name))
                {
                    var valueType = field.FieldType;
                    field.SetValue(entity, reader[field.Name].CastTo(valueType));
                }
            }

            return entity;
        }

        /// <summary>
        /// To a set of entity
        /// </summary>
        /// <param name="reader"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> ToEntities<T>(this IDataReader reader) where T : new()
        {
            reader.CheckNull(nameof(reader));

            var type = typeof(T);
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);

            var list = new List<T>();

            var hash = new HashSet<string>(Enumerable.Range(0, reader.FieldCount).Select(reader.GetName));

            while (reader.Read())
            {
                var entity = new T();

                foreach (var property in properties)
                {
                    if (hash.Contains(property.Name))
                    {
                        var valueType = property.PropertyType;
                        property.SetValue(entity, reader[property.Name].CastTo(valueType), null);
                    }
                }

                foreach (var field in fields)
                {
                    if (hash.Contains(field.Name))
                    {
                        var valueType = field.FieldType;
                        field.SetValue(entity, reader[field.Name].CastTo(valueType));
                    }
                }

                list.Add(entity);
            }

            return list;
        }

        /// <summary>
        /// To expando object
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static dynamic ToExpandoObject(this IDataReader reader)
        {
            reader.CheckNull(nameof(reader));

            var columnNames = Enumerable.Range(0, reader.FieldCount)
               .Select(x => new KeyValuePair<int, string>(x, reader.GetName(x)))
               .ToDictionary(pair => pair.Key);

            dynamic entity = new ExpandoObject();
            var expandoDict = (IDictionary<string, object>) entity;

            Enumerable.Range(0, reader.FieldCount)
               .ToList()
               .ForEach(x => expandoDict.Add(columnNames[x].Value, reader[x]));

            return entity;
        }

        /// <summary>
        /// To a set of expando object
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static IEnumerable<dynamic> ToExpandoObjects(this IDataReader reader)
        {
            reader.CheckNull(nameof(reader));

            var columnNames = Enumerable.Range(0, reader.FieldCount)
               .Select(x => new KeyValuePair<int, string>(x, reader.GetName(x)))
               .ToDictionary(pair => pair.Key);

            var list = new List<dynamic>();

            while (reader.Read())
            {
                dynamic entity = new ExpandoObject();
                var expandoDict = (IDictionary<string, object>) entity;

                Enumerable.Range(0, reader.FieldCount)
                   .ToList()
                   .ForEach(x => expandoDict.Add(columnNames[x].Value, reader[x]));

                list.Add(entity);
            }

            return list;
        }

        /// <summary>
        /// Gets column name
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetColumnNames(this IDataRecord reader)
        {
            reader.CheckNull(nameof(reader));
            return Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();
        }

        /// <summary>
        /// Gets value as...
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="index"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetValueAs<T>(this IDataReader reader, int index)
        {
            reader.CheckNull(nameof(reader));
            return (T) reader.GetValue(index);
        }

        /// <summary>
        /// Gets value as...
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="columnName"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetValueAs<T>(this IDataReader reader, string columnName)
        {
            reader.CheckNull(nameof(reader));
            return (T) reader.GetValue(reader.GetOrdinal(columnName));
        }

        /// <summary>
        /// Gets value as... or default...
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="index"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetValueAsOrDefault<T>(this IDataReader reader, int index)
        {
            reader.CheckNull(nameof(reader));
            return Try
               .Create(() => (T) reader.GetValue(index))
               .GetSafeValue();
        }

        /// <summary>
        /// Gets value as... or default...
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="index"></param>
        /// <param name="defaultValue"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetValueAsOrDefault<T>(this IDataReader reader, int index, T defaultValue)
        {
            reader.CheckNull(nameof(reader));
            return Try
               .Create(() => (T) reader.GetValue(index))
               .GetSafeValue(defaultValue);
        }

        /// <summary>
        /// Gets value as... or default...
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="index"></param>
        /// <param name="defaultValueFactory"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetValueAsOrDefault<T>(this IDataReader reader, int index, Func<IDataReader, int, T> defaultValueFactory)
        {
            reader.CheckNull(nameof(reader));
            return Try
               .Create(() => (T) reader.GetValue(index))
               .GetSafeValue(() => defaultValueFactory(reader, index));
        }

        /// <summary>
        /// Gets value as... or default...
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="columnName"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetValueAsOrDefault<T>(this IDataReader reader, string columnName)
        {
            reader.CheckNull(nameof(reader));
            return Try
               .Create(() => (T) reader.GetValue(reader.GetOrdinal(columnName)))
               .GetSafeValue();
        }

        /// <summary>
        /// Gets value as... or default value
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="columnName"></param>
        /// <param name="defaultValue"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetValueAsOrDefault<T>(this IDataReader reader, string columnName, T defaultValue)
        {
            reader.CheckNull(nameof(reader));
            return Try
               .Create(() => (T) reader.GetValue(reader.GetOrdinal(columnName)))
               .GetSafeValue(defaultValue);
        }

        /// <summary>
        /// Gets value as... or default value
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="columnName"></param>
        /// <param name="defaultValueFactory"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetValueAsOrDefault<T>(this IDataReader reader, string columnName, Func<IDataReader, string, T> defaultValueFactory)
        {
            reader.CheckNull(nameof(reader));
            return Try
               .Create(() => (T) reader.GetValue(reader.GetOrdinal(columnName)))
               .GetSafeValue(() => defaultValueFactory(reader, columnName));
        }

        /// <summary>
        /// Gets value to...
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="index"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetValueTo<T>(this IDataReader reader, int index)
        {
            reader.CheckNull(nameof(reader));
            return reader.GetValue(index).CastTo<T>();
        }

        /// <summary>
        /// Gets value to...
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="columnName"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetValueTo<T>(this IDataReader reader, string columnName)
        {
            reader.CheckNull(nameof(reader));
            return reader.GetValue(reader.GetOrdinal(columnName)).CastTo<T>();
        }

        /// <summary>
        /// Gets value to...or default...
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="index"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetValueToOrDefault<T>(this IDataReader reader, int index)
        {
            reader.CheckNull(nameof(reader));
            return Try
               .Create(() => reader.GetValue(index).CastTo<T>())
               .GetSafeValue();
        }

        /// <summary>
        /// Gets value to...or default vaule
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="index"></param>
        /// <param name="defaultValue"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetValueToOrDefault<T>(this IDataReader reader, int index, T defaultValue)
        {
            reader.CheckNull(nameof(reader));
            return Try
               .Create(() => reader.GetValue(index).CastTo<T>())
               .GetSafeValue(defaultValue);
        }

        /// <summary>
        /// Gets value to...or default value
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="index"></param>
        /// <param name="defaultValueFactory"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetValueToOrDefault<T>(this IDataReader reader, int index, Func<IDataReader, int, T> defaultValueFactory)
        {
            reader.CheckNull(nameof(reader));
            return Try
               .Create(() => reader.GetValue(index).CastTo<T>())
               .GetSafeValue(() => defaultValueFactory(reader, index));
        }

        /// <summary>
        /// Gets value to...or default...
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="columnName"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetValueToOrDefault<T>(this IDataReader reader, string columnName)
        {
            reader.CheckNull(nameof(reader));
            return Try
               .Create(() => reader.GetValue(reader.GetOrdinal(columnName)).CastTo<T>())
               .GetSafeValue();
        }

        /// <summary>
        /// Gets value to...or default value
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="columnName"></param>
        /// <param name="defaultValue"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetValueToOrDefault<T>(this IDataReader reader, string columnName, T defaultValue)
        {
            reader.CheckNull(nameof(reader));
            return Try
               .Create(() => reader.GetValue(reader.GetOrdinal(columnName)).CastTo<T>())
               .GetSafeValue(defaultValue);
        }

        /// <summary>
        /// Gets value to...or default value
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="columnName"></param>
        /// <param name="defaultValueFactory"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetValueToOrDefault<T>(this IDataReader reader, string columnName, Func<IDataReader, string, T> defaultValueFactory)
        {
            reader.CheckNull(nameof(reader));
            return Try
               .Create(() => reader.GetValue(reader.GetOrdinal(columnName)).CastTo<T>())
               .GetSafeValue(() => defaultValueFactory(reader, columnName));
        }

        /// <summary>
        /// For each
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IDataReader ForEach(this IDataReader reader, Action<IDataReader> action)
        {
            reader.CheckNull(nameof(reader));
            if (action is null)
                return reader;
            while (reader.Read())
                action(reader);
            return reader;
        }

        /// <summary>
        /// Contains column
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        public static bool ContainsColumn(this IDataReader reader, int columnIndex)
        {
            reader.CheckNull(nameof(reader));
            return Try
               .Create(() => reader.FieldCount > columnIndex)
               .Recover(ex => reader[columnIndex] != null)
               .IsSuccess;
        }

        /// <summary>
        /// Contains column
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static bool ContainsColumn(this IDataReader reader, string columnName)
        {
            reader.CheckNull(nameof(reader));
            return Try
               .Create(() => reader.GetOrdinal(columnName) != -1)
               .Recover(ex => reader[columnName] != null)
               .IsSuccess;
        }
    }
}