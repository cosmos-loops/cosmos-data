using System;
using System.Linq;

namespace Cosmos.Data.Predicate
{
    /// <summary>
    /// 表示条件项
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class SQLConditionItem
    {
        /// <summary>
        /// 获取或设置属性名称
        /// </summary>
        public string MemberName { get; set; }

        /// <summary>
        /// 获取或设置条件值
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 获取或设置比较操作符
        /// </summary>
        public SQLOperator? Operator { get; set; }

        /// <summary>
        /// 转换为泛型的ConditionItem
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="NotSupportedException"></exception>
        /// <returns></returns>
        public SQLConditionItem<T> AsGeneric<T>()
        {
            var member = SQLConditionItem<T>
               .TypeProperties
               .FirstOrDefault(item => item.Name.Equals(MemberName, StringComparison.OrdinalIgnoreCase));

            if (member is null)
                return null;

            return new SQLConditionItem<T>(member, Value, Operator);
        }

        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{MemberName} {Operator} {Value}";
        }
    }
}