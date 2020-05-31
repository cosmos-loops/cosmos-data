using System;
using System.Collections;
using System.Linq;
using System.Reflection;

/*
 * Reference to:
 *     SmartSql
 *     Author: ahoo-wang
 *     Url: https://github.com/dotnetcore/smartsql/
 */

namespace Cosmos.Data.Sx.MySql.SqlBulkCopy
{
    internal static class CommonType
    {
        public static readonly Type Object = TypeClass.ObjectClass;
        public static readonly Type Int32 = TypeClass.Int32Class;
        public static readonly Type Int64 = TypeClass.Int64Class;
        public static readonly Type Boolean = TypeClass.BooleanClass;
        public static readonly Type String = TypeClass.StringClass;
        public static readonly Type DateTime = TypeClass.DateTimeClass;
        public static readonly Type Guid = TypeClass.GuidClass;
        public static readonly Type TimeSpan = TypeClass.TimeSpanClass;
        public static readonly Type Enum = TypeClass.EnumClass;
        public static readonly Type ObjectArray = TypeClass.ObjectArrayClass;

        // ReSharper disable once InconsistentNaming
        public static readonly Type IEnumerable = typeof(IEnumerable);
        public static readonly Type GenericList = TypeClass.GenericListClass;
        public static readonly Type Task = TypeClass.TaskClass;
        public static readonly Type Void = TypeClass.VoidClass;
        public static readonly Type ValueTuple = TypeClass.ValueTupleClass;

        public static bool IsValueTuple(Type type)
        {
            return type != null && type.ToString().StartsWith("System.ValueTuple");
        }

        public static MethodInfo GetValueTupleCreateMethod(Type valueTupleType)
        {
            return GetValueTupleCreateMethod(valueTupleType.GenericTypeArguments);
        }

        public static MethodInfo GetValueTupleCreateMethod(Type[] resultGenericTypeArguments)
        {
            return ValueTuple.GetMethods().First(m =>
            {
                if (m.Name != "Create")
                {
                    return false;
                }

                return m.GetParameters().Length == resultGenericTypeArguments.Length;
            }).MakeGenericMethod(resultGenericTypeArguments);
        }
    }
}