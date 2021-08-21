using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Cosmos.Data.Predicate
{
    /// <summary>
    /// 提供谓词筛选表达式的生成
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public static class SQLPredicate
    {
        /// <summary>
        /// Enumerable的Contains方法
        /// </summary>
        private static readonly MethodInfo containsMethod;

        /// <summary>
        /// 获取谓词筛选表达式的参数名
        /// </summary>
        public static string ParameterName => "item";

        /// <summary>
        /// 静态构造器
        /// </summary>
        static SQLPredicate()
        {
            var q = from m in typeof(Enumerable).GetMethods()
                    where m.Name == nameof(Enumerable.Contains) && m.IsGenericMethod
                    let parameters = m.GetParameters()
                    where parameters.Length == 2
                    let pLast = parameters.Last()
                    where pLast.ParameterType.GetTypeInfo().IsGenericType == false
                    select m;

            containsMethod = q.Single();
        }

        /// <summary>
        /// 返回默认为True的谓词筛选表达式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Expression<Func<T, bool>> True<T>() => item => true;

        /// <summary>
        /// 返回默认为False的谓词筛选表达式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Expression<Func<T, bool>> False<T>() => item => false;

        /// <summary>
        /// 将数组转换为Or的相等的谓词筛选表达式
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <typeparam name="TKey">属性类型</typeparam>
        /// <param name="keySelector">属性选择</param>
        /// <param name="values">包含的值</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns></returns>
        public static Expression<Func<T, bool>> CreateOrEqual<T, TKey>(Expression<Func<T, TKey>> keySelector, IEnumerable<TKey> values)
        {
            if (keySelector is null)
                throw new ArgumentNullException(nameof(keySelector));

            if (values is null)
                throw new ArgumentNullException(nameof(values));

            // ReSharper disable once PossibleMultipleEnumeration
            if (values.Any() == false)
                throw new ArgumentOutOfRangeException(nameof(values));

            var parameter = keySelector.Parameters.Single();
            // ReSharper disable once PossibleMultipleEnumeration
            var expressions = values.Select(value => (Expression) Expression.Equal(keySelector.Body, ConstantExpression(value, typeof(TKey))));
            var body = expressions.Aggregate(Expression.OrElse);
            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }

        /// <summary>
        /// 将数组转换为And的不等的谓词筛选表达式
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <typeparam name="TKey">>属性类型</typeparam>
        /// <param name="keySelector">属性选择</param>
        /// <param name="values">包含的值</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns></returns>
        public static Expression<Func<T, bool>> CreateAndNotEqual<T, TKey>(Expression<Func<T, TKey>> keySelector, IEnumerable<TKey> values)
        {
            if (keySelector is null)
                throw new ArgumentNullException(nameof(keySelector));

            if (values is null)
                throw new ArgumentNullException(nameof(values));

            // ReSharper disable once PossibleMultipleEnumeration
            if (values.Any() == false)
                throw new ArgumentOutOfRangeException(nameof(values));

            var parameter = keySelector.Parameters.Single();
            // ReSharper disable once PossibleMultipleEnumeration
            var expressions = values.Select(value => (Expression) Expression.NotEqual(keySelector.Body, ConstantExpression(value, typeof(TKey))));
            var body = expressions.Aggregate(Expression.AndAlso);
            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }

        /// <summary>
        /// 生成In操作的谓词筛选表达式
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <typeparam name="TKey">属性类型</typeparam>
        /// <param name="keySelector">属性选择</param>
        /// <param name="values">包含的值</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns></returns>
        public static Expression<Func<T, bool>> CreateContains<T, TKey>(Expression<Func<T, TKey>> keySelector, IEnumerable<TKey> values)
        {
            if (keySelector is null)
                throw new ArgumentNullException(nameof(keySelector));

            if (values is null)
                throw new ArgumentNullException(nameof(values));

            if (values.Any() == false)
                throw new ArgumentOutOfRangeException(nameof(values));

            var method = containsMethod.MakeGenericMethod(typeof(TKey));
            var body = Expression.Call(null, method, Expression.Constant(values, typeof(IEnumerable<TKey>)), keySelector.Body);
            var parameter = keySelector.Parameters.Single();
            return Expression.Lambda(body, parameter) as Expression<Func<T, bool>>;
        }

        /// <summary>
        /// 根据属性名生成谓词筛选表达式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName">属性名</param>
        /// <param name="value">值</param>
        /// <param name="operator">操作符</param>
        /// <exception cref="MissingFieldException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Create<T>(string propertyName, object value, SQLOperator @operator)
        {
            var member = SQLConditionItem<T>.TypeProperties.FirstOrDefault(item => item.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase));
            if (member is null)
                throw new MissingFieldException(propertyName);
            return Create<T>(member, value, @operator);
        }

        /// <summary>
        /// 根据属性生成谓词筛选表达式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="member">属性成员</param>
        /// <param name="value">值</param>
        /// <param name="operator">操作符</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Create<T>(MemberInfo member, object value, SQLOperator @operator)
        {
            if (member is null)
                throw new ArgumentNullException(nameof(member));
            var parameter = Expression.Parameter(typeof(T), ParameterName);
            var memberExpression = Expression.MakeMemberAccess(parameter, member);
            return Create<T>(parameter, memberExpression, value, @operator);
        }

        /// <summary>
        /// 根据属性生成谓词筛选表达式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="keySelector">属性选择</param>
        /// <param name="value">值</param>
        /// <param name="operator">操作符</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Create<T, TKey>(Expression<Func<T, TKey>> keySelector, TKey value, SQLOperator @operator)
        {
            if (keySelector is null)
                throw new ArgumentNullException(nameof(keySelector));
            if (keySelector.Body.NodeType != ExpressionType.MemberAccess)
                throw new ArgumentException("要求表达式主体必须为MemberAccess表达式", nameof(keySelector));
            return Create<T>(keySelector.Parameters.Single(), (MemberExpression) keySelector.Body, value, @operator);
        }

        /// <summary>
        /// 生成表达式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameter">参数表达式</param>
        /// <param name="member">成员表达式</param>
        /// <param name="value">属性值</param>
        /// <param name="operator">操作符</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Create<T>(ParameterExpression parameter, MemberExpression member, object value, SQLOperator @operator)
        {
            if (parameter is null)
                throw new ArgumentNullException(nameof(parameter));

            if (member is null)
                throw new ArgumentNullException(nameof(member));

            switch (@operator)
            {
                case SQLOperator.Contains:
                case SQLOperator.EndWith:
                case SQLOperator.StartsWith:
                    if (value != null && value.GetType() != typeof(string))
                        throw new NotSupportedException($"{nameof(SQLOperator)}.{@operator}只适用于string类型");

                    var method = typeof(string).GetMethod(@operator.ToString(), new[] {typeof(string)});
                    var body = Expression.Call(member, method, ConstantExpression(value, typeof(string)));
                    return Expression.Lambda(body, parameter) as Expression<Func<T, bool>>;

                default:
                    var valueExp = ConstantExpression(value, member.Type);
                    var binaryBody = Expression.MakeBinary((ExpressionType) @operator, member, valueExp);
                    return Expression.Lambda(binaryBody, parameter) as Expression<Func<T, bool>>;
            }
        }

        /// <summary>
        /// 生成常量表达式
        /// 使用属性访问表达式替代常量访问
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        private static Expression ConstantExpression(object value, Type targetType)
        {
            if (targetType is null)
                throw new ArgumentNullException(nameof(targetType));

            if (value is null)
                return Expression.Constant(value, targetType);

            if (targetType == typeof(string))
            {
                var constantString = Expression.Constant(new ConstantString(value));
                return Expression.Property(constantString, nameof(ConstantString.Value));
            }

            var expression = (Expression) Expression.Constant(value);
            return value.GetType() == targetType ? expression : Expression.Convert(expression, targetType);
        }

        /// <summary>
        /// 表示文本常量
        /// </summary>
        private class ConstantString
        {
            /// <summary>
            /// 获取常量值
            /// </summary>
            public string Value { get; }

            /// <summary>
            /// 文本常量
            /// </summary>
            /// <param name="value">常量值</param>
            public ConstantString(object value)
            {
                Value = value?.ToString();
            }

            /// <summary>
            /// 转换为字符串
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return $@"""{Value}""";
            }
        }
    }
}