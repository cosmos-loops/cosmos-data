using System.Collections.Generic;
using System.Data.Common;
using Cosmos;

namespace System.Data
{
    /// <summary>
    /// Extensions for <see cref="DbCommand"/>
    /// </summary>
    public static class DbCommandExtensions
    {
        /// <summary>
        /// Execute entity
        /// </summary>
        /// <param name="this"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteEntity<T>(this DbCommand @this) where T : new()
        {
            using (IDataReader reader = @this.ExecuteReader())
            {
                reader.Read();
                return reader.ToEntity<T>();
            }
        }

        /// <summary>
        /// Execute a set of entity
        /// </summary>
        /// <param name="this"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> ExecuteEntities<T>(this DbCommand @this) where T : new()
        {
            using (IDataReader reader = @this.ExecuteReader())
            {
                return reader.ToEntities<T>();
            }
        }

        /// <summary>
        /// Execute expando object
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static dynamic ExecuteExpandoObject(this DbCommand @this)
        {
            using (IDataReader reader = @this.ExecuteReader())
            {
                reader.Read();
                return reader.ToExpandoObject();
            }
        }

        /// <summary>
        /// Execute a set of expando object
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static IEnumerable<dynamic> ExecuteExpandoObjects(this DbCommand @this)
        {
            using (IDataReader reader = @this.ExecuteReader())
            {
                return reader.ToExpandoObjects();
            }
        }

        /// <summary>
        /// Execute scalar as...
        /// </summary>
        /// <param name="this"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteScalarAs<T>(this DbCommand @this) => (T) @this.ExecuteScalar();

        /// <summary>
        /// Execute scalar as... or default...
        /// </summary>
        /// <param name="this"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteScalarAsOrDefault<T>(this DbCommand @this)
        {
            try
            {
                return (T) @this.ExecuteScalar();
            }
            catch (Exception)
            {
                return default;
            }
        }

        /// <summary>
        /// Execute scalar as... or default value
        /// </summary>
        /// <param name="this"></param>
        /// <param name="defaultValue"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteScalarAsOrDefault<T>(this DbCommand @this, T defaultValue)
        {
            try
            {
                return (T) @this.ExecuteScalar();
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Execute scalar as... or default value
        /// </summary>
        /// <param name="this"></param>
        /// <param name="defaultValueFactory"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteScalarAsOrDefault<T>(this DbCommand @this, Func<DbCommand, T> defaultValueFactory)
        {
            try
            {
                return (T) @this.ExecuteScalar();
            }
            catch (Exception)
            {
                return defaultValueFactory(@this);
            }
        }

        /// <summary>
        /// Execute scalar to...
        /// </summary>
        /// <param name="this"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteScalarTo<T>(this DbCommand @this) => @this.ExecuteScalar().CastTo<T>();

        /// <summary>
        /// Execute scalar to... or default...
        /// </summary>
        /// <param name="this"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteScalarToOrDefault<T>(this DbCommand @this)
        {
            try
            {
                return @this.ExecuteScalar().CastTo<T>();
            }
            catch (Exception)
            {
                return default;
            }
        }

        /// <summary>
        /// Execute scalar to... or default value
        /// </summary>
        /// <param name="this"></param>
        /// <param name="defaultValue"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteScalarToOrDefault<T>(this DbCommand @this, T defaultValue)
        {
            try
            {
                return @this.ExecuteScalar().CastTo<T>();
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Execute scalar to... or default value
        /// </summary>
        /// <param name="this"></param>
        /// <param name="defaultValueFactory"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteScalarToOrDefault<T>(this DbCommand @this, Func<DbCommand, T> defaultValueFactory)
        {
            try
            {
                return @this.ExecuteScalar().CastTo<T>();
            }
            catch (Exception)
            {
                return defaultValueFactory(@this);
            }
        }
    }
}