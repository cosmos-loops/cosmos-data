using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Cosmos.Conversions;
using Cosmos.Exceptions;

namespace Cosmos.Data.Sx
{
    /// <summary>
    /// Extensions for <see cref="DbCommand"/>
    /// </summary>
    public static class DbCommandExtensions
    {
        #region Execute Entity

        /// <summary>
        /// Execute entity
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteEntity<T>(this DbCommand command) where T : new()
        {
            command.CheckNull(nameof(command));
            using IDataReader reader = command.ExecuteReader();
            reader.Read();
            return reader.ToEntity<T>();
        }

        /// <summary>
        /// Execute a set of entity
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> ExecuteEntities<T>(this DbCommand command) where T : new()
        {
            command.CheckNull(nameof(command));
            using IDataReader reader = command.ExecuteReader();
            return reader.ToEntities<T>();
        }

        /// <summary>
        /// Execute entity
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async Task<T> ExecuteEntityAsync<T>(this DbCommand command) where T : new()
        {
            command.CheckNull(nameof(command));
            using IDataReader reader = await command.ExecuteReaderAsync();
            reader.Read();
            return reader.ToEntity<T>();
        }

        /// <summary>
        /// Execute a set of entity
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async Task<IEnumerable<T>> ExecuteEntitiesAsync<T>(this DbCommand command) where T : new()
        {
            command.CheckNull(nameof(command));
            using IDataReader reader = await command.ExecuteReaderAsync();
            return reader.ToEntities<T>();
        }

        #endregion

        #region Execute ExpandoObject

        /// <summary>
        /// Execute expando object
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static dynamic ExecuteExpandoObject(this DbCommand command)
        {
            command.CheckNull(nameof(command));
            using IDataReader reader = command.ExecuteReader();
            reader.Read();
            return reader.ToExpandoObject();
        }

        /// <summary>
        /// Execute a set of expando object
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static IEnumerable<dynamic> ExecuteExpandoObjects(this DbCommand command)
        {
            command.CheckNull(nameof(command));
            using IDataReader reader = command.ExecuteReader();
            return reader.ToExpandoObjects();
        }

        /// <summary>
        /// Execute expando object
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static async Task<dynamic> ExecuteExpandoObjectAsync(this DbCommand command)
        {
            command.CheckNull(nameof(command));
            using IDataReader reader = await command.ExecuteReaderAsync();
            reader.Read();
            return reader.ToExpandoObject();
        }

        /// <summary>
        /// Execute a set of expando object
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<dynamic>> ExecuteExpandoObjectsAsync(this DbCommand command)
        {
            command.CheckNull(nameof(command));
            using IDataReader reader = await command.ExecuteReaderAsync();
            return reader.ToExpandoObjects();
        }

        #endregion

        #region Execute Scalar As

        /// <summary>
        /// Execute scalar as...
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteScalarAs<T>(this DbCommand command)
        {
            command.CheckNull(nameof(command));
            return (T) command.ExecuteScalar();
        }

        /// <summary>
        /// Execute scalar as... or default...
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteScalarAsOrDefault<T>(this DbCommand command)
        {
            command.CheckNull(nameof(command));
            return Try
               .Create(() => (T) command.ExecuteScalar())
               .GetSafeValue();
        }

        /// <summary>
        /// Execute scalar as... or default value
        /// </summary>
        /// <param name="command"></param>
        /// <param name="defaultValue"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteScalarAsOrDefault<T>(this DbCommand command, T defaultValue)
        {
            command.CheckNull(nameof(command));
            return Try
               .Create(() => (T) command.ExecuteScalar())
               .GetSafeValue(defaultValue);
        }

        /// <summary>
        /// Execute scalar as... or default value
        /// </summary>
        /// <param name="command"></param>
        /// <param name="defaultValueFactory"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteScalarAsOrDefault<T>(this DbCommand command, Func<DbCommand, T> defaultValueFactory)
        {
            command.CheckNull(nameof(command));
            return Try
               .Create(() => (T) command.ExecuteScalar())
               .GetSafeValue(() => defaultValueFactory(command));
        }

        /// <summary>
        /// Execute scalar as...
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async Task<T> ExecuteScalarAsAsync<T>(this DbCommand command)
        {
            command.CheckNull(nameof(command));
            return (T) await command.ExecuteScalarAsync();
        }

        /// <summary>
        /// Execute scalar as... or default...
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async Task<T> ExecuteScalarAsOrDefaultAsync<T>(this DbCommand command)
        {
            command.CheckNull(nameof(command));
            return await Try
               .CreateFromTask(async () => (T) await command.ExecuteScalarAsync())
               .GetSafeValueAsync();
        }

        /// <summary>
        /// Execute scalar as... or default value
        /// </summary>
        /// <param name="command"></param>
        /// <param name="defaultValue"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async Task<T> ExecuteScalarAsOrDefaultAsync<T>(this DbCommand command, T defaultValue)
        {
            command.CheckNull(nameof(command));
            return await Try
               .CreateFromTask(async () => (T) await command.ExecuteScalarAsync())
               .GetSafeValueAsync(defaultValue);
        }

        /// <summary>
        /// Execute scalar as... or default value
        /// </summary>
        /// <param name="command"></param>
        /// <param name="defaultValueFactory"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async Task<T> ExecuteScalarAsOrDefaultAsync<T>(this DbCommand command, Func<DbCommand, T> defaultValueFactory)
        {
            command.CheckNull(nameof(command));
            return await Try
               .CreateFromTask(async () => (T) await command.ExecuteScalarAsync())
               .GetSafeValueAsync(() => defaultValueFactory(command));
        }

        #endregion

        #region Execute Scalar To

        /// <summary>
        /// Execute scalar to...
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteScalarTo<T>(this DbCommand command)
        {
            command.CheckNull(nameof(command));
            return command.ExecuteScalar().CastTo<T>();
        }

        /// <summary>
        /// Execute scalar to... or default...
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteScalarToOrDefault<T>(this DbCommand command)
        {
            command.CheckNull(nameof(command));
            return Try
               .Create(() => command.ExecuteScalar().CastTo<T>())
               .GetSafeValue();
        }

        /// <summary>
        /// Execute scalar to... or default value
        /// </summary>
        /// <param name="command"></param>
        /// <param name="defaultValue"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteScalarToOrDefault<T>(this DbCommand command, T defaultValue)
        {
            command.CheckNull(nameof(command));
            return Try
               .Create(() => command.ExecuteScalar().CastTo<T>())
               .GetSafeValue(defaultValue);
        }

        /// <summary>
        /// Execute scalar to... or default value
        /// </summary>
        /// <param name="command"></param>
        /// <param name="defaultValueFactory"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteScalarToOrDefault<T>(this DbCommand command, Func<DbCommand, T> defaultValueFactory)
        {
            command.CheckNull(nameof(command));
            return Try
               .Create(() => command.ExecuteScalar().CastTo<T>())
               .GetSafeValue(() => defaultValueFactory(command));
        }

        /// <summary>
        /// Execute scalar to...
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async Task<T> ExecuteScalarToAsync<T>(this DbCommand command)
        {
            command.CheckNull(nameof(command));
            return (await command.ExecuteScalarAsync()).CastTo<T>();
        }

        /// <summary>
        /// Execute scalar to... or default...
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async Task<T> ExecuteScalarToOrDefaultAsync<T>(this DbCommand command)
        {
            command.CheckNull(nameof(command));
            return await Try
               .CreateFromTask(async () => (await command.ExecuteScalarAsync()).CastTo<T>())
               .GetSafeValueAsync();
        }

        /// <summary>
        /// Execute scalar to... or default value
        /// </summary>
        /// <param name="command"></param>
        /// <param name="defaultValue"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async Task<T> ExecuteScalarToOrDefaultAsync<T>(this DbCommand command, T defaultValue)
        {
            command.CheckNull(nameof(command));
            return await Try
               .CreateFromTask(async () => (await command.ExecuteScalarAsync()).CastTo<T>())
               .GetSafeValueAsync(defaultValue);
        }

        /// <summary>
        /// Execute scalar to... or default value
        /// </summary>
        /// <param name="command"></param>
        /// <param name="defaultValueFactory"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async Task<T> ExecuteScalarToOrDefaultAsync<T>(this DbCommand command, Func<DbCommand, T> defaultValueFactory)
        {
            command.CheckNull(nameof(command));
            return await Try
               .CreateFromTask(async () => (await command.ExecuteScalarAsync()).CastTo<T>())
               .GetSafeValueAsync(() => defaultValueFactory(command));
        }

        #endregion
    }
}