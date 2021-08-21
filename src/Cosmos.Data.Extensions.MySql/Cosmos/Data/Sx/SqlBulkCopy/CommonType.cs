using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using Cosmos.Reflection;

/*
 * Reference to:
 *     SmartSql
 *     Author: ahoo-wang
 *     Url: https://github.com/dotnetcore/smartsql/
 */

namespace Cosmos.Data.Sx.SqlBulkCopy
{
    internal static class CommonType
    {
        public static readonly Type Object = TypeClass.ObjectClazz;
        public static readonly Type Int32 = TypeClass.Int32Clazz;
        public static readonly Type Int64 = TypeClass.Int64Clazz;
        public static readonly Type Boolean = TypeClass.BooleanClazz;
        public static readonly Type String = TypeClass.StringClazz;
        public static readonly Type DateTime = TypeClass.DateTimeClazz;
        public static readonly Type Guid = TypeClass.GuidClazz;
        public static readonly Type TimeSpan = TypeClass.TimeSpanClazz;
        public static readonly Type Enum = TypeClass.EnumClazz;
        public static readonly Type ObjectArray = TypeClass.ObjectArrayClazz;

        // ReSharper disable once InconsistentNaming
        public static readonly Type IEnumerable = typeof(IEnumerable);
        public static readonly Type GenericList = TypeClass.GenericListClazz;
        public static readonly Type Task = TypeClass.TaskClazz;
        public static readonly Type Void = TypeClass.VoidClazz;
        public static readonly Type ValueTuple = TypeClass.ValueTupleClazz;

        public static bool IsValueTuple(Type type)
        {
            return type is not null && type.ToString().StartsWith("System.ValueTuple");
        }

        public static MethodInfo GetValueTupleCreateMethod(Type valueTupleType)
        {
            return GetValueTupleCreateMethod(valueTupleType.GenericTypeArguments);
        }

        public static MethodInfo GetValueTupleCreateMethod(Type[] resultGenericTypeArguments)
        {
            return ValueTuple.GetMethods().First(m =>
            {
                if (m.Name == "Create")
                    return m.GetParameters().Length == resultGenericTypeArguments.Length;
                return false;
            }).MakeGenericMethod(resultGenericTypeArguments);
        }
    }
}