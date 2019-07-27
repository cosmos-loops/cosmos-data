using System.Collections.Generic;
using System.Data.Common;
using Cosmos;

namespace System.Data
{
    public static class DbCommandExtensions
    {
        public static T ExecuteEntity<T>(this DbCommand @this) where T : new()
        {
            using (IDataReader reader = @this.ExecuteReader())
            {
                reader.Read();
                return reader.ToEntity<T>();
            }
        }

        public static IEnumerable<T> ExecuteEntities<T>(this DbCommand @this) where T : new()
        {
            using (IDataReader reader = @this.ExecuteReader())
            {
                return reader.ToEntities<T>();
            }
        }
        
        public static dynamic ExecuteExpandoObject(this DbCommand @this)
        {
            using (IDataReader reader = @this.ExecuteReader())
            {
                reader.Read();
                return reader.ToExpandoObject();
            }
        }
        
        public static IEnumerable<dynamic> ExecuteExpandoObjects(this DbCommand @this)
        {
            using (IDataReader reader = @this.ExecuteReader())
            {
                return reader.ToExpandoObjects();
            }
        }
        
        public static T ExecuteScalarAs<T>(this DbCommand @this)
        {
            return (T) @this.ExecuteScalar();
        }
        
        public static T ExecuteScalarAsOrDefault<T>(this DbCommand @this)
        {
            try
            {
                return (T) @this.ExecuteScalar();
            }
            catch (Exception)
            {
                return default(T);
            }
        }

       
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
        
        public static T ExecuteScalarTo<T>(this DbCommand @this)
        {
            return @this.ExecuteScalar().CastTo<T>();
        }
        
        public static T ExecuteScalarToOrDefault<T>(this DbCommand @this)
        {
            try
            {
                return @this.ExecuteScalar().CastTo<T>();
            }
            catch (Exception)
            {
                return default(T);
            }
        }

     
        
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