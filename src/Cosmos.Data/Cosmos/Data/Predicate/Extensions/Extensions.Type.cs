using System;

// ReSharper disable once CheckNamespace
namespace Cosmos.Data.Predicate
{
    /// <summary>
    /// 类型扩展
    /// </summary>
    internal static class TypeExtensions
    {
        /// <summary>
        /// 是否可以从TBase类型派生
        /// </summary>
        /// <typeparam name="TBase"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsInheritFrom<TBase>(this Type type) => typeof(TBase).IsAssignableFrom(type);
    }
}