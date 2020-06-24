using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Cosmos.Data.Predicate
{
    /// <summary>
    /// 表示条件项
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class SQLConditionItem<T>
    {
        /// <summary>
        /// 获取T类型的所有属性
        /// </summary>
        public readonly static PropertyInfo[] TypeProperties = typeof(T).GetProperties();

        /// <summary>
        /// 获取属性
        /// </summary>
        public PropertyInfo Member { get; }

        /// <summary>
        /// 获取条件值
        /// </summary>
        public object Value { get; }

        /// <summary>
        /// 获取或设置比较操作符
        /// </summary>
        public SQLOperator Operator { get; set; }

        /// <summary>
        /// 条件项
        /// </summary>
        /// <param name="member">属性</param>
        /// <param name="value">条件值</param>
        /// <param name="operator">比较操作符</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        public SQLConditionItem(PropertyInfo member, object value, SQLOperator? @operator)
        {
            Member = member ?? throw new ArgumentNullException(nameof(member));
            Value = ConvertToType(value, member.PropertyType);

            if (@operator.HasValue)
            {
                Operator = @operator.Value;
            }
            else
            {
                Operator = member.PropertyType == TypeClass.StringClass ? SQLOperator.Contains : SQLOperator.Equal;
            }
        }

        /// <summary>
        /// 将value转换为目标类型
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <param name="targetType">转换的目标类型</param>
        /// <exception cref="NotSupportedException"></exception>
        /// <returns></returns>
        private static object ConvertToType(object value, Type targetType)
        {
            if (value is null)
                return null;

            if (value.GetType() == targetType)
                return value;

            var underlyingType = Nullable.GetUnderlyingType(targetType);
            if (underlyingType != null)
                targetType = underlyingType;

            if (targetType.GetTypeInfo().IsEnum)
                return Enum.Parse(targetType, value.ToString(), true);

            if (value is IConvertible convertible && targetType.IsInheritFrom<IConvertible>())
                return convertible.ToType(targetType, null);

            if (TypeClass.GuidClass == targetType)
                return Guid.Parse(value.ToString());

            if (TypeClass.DateTimeOffsetClass == targetType)
                return DateTimeOffset.Parse(value.ToString());

            throw new NotSupportedException($"不支持将对象{value}转换为{targetType}");
        }

        /// <summary>
        /// 转换为谓词筛选表达式
        /// </summary>
        /// <exception cref="NotSupportedException"></exception>
        /// <returns></returns>
        public Expression<Func<T, bool>> ToPredicate() => SQLPredicate.Create<T>(Member, Value, Operator);

        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString() => ToPredicate().ToString();
    }
}