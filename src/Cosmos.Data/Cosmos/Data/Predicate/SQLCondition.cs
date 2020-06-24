using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Cosmos.Data.Predicate
{
    /// <summary>
    /// 表示查询条件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    // ReSharper disable once InconsistentNaming
    public class SQLCondition<T>
    {
        /// <summary>
        /// 获取查询条件项
        /// </summary>
        public IList<SQLConditionItem<T>> Items { get; } = new List<SQLConditionItem<T>>();

        /// <summary>
        /// 查询条件
        /// </summary>
        /// <param name="conditionItems">查询条件项</param>
        public SQLCondition(IEnumerable<KeyValuePair<string, string>> conditionItems)
            : this(GetConditionItems(conditionItems)) { }

        /// <summary>
        /// 查询条件
        /// </summary>
        /// <param name="conditionItems">查询条件项</param>
        public SQLCondition(IEnumerable<KeyValuePair<string, object>> conditionItems)
            : this(GetConditionItems(conditionItems)) { }

        /// <summary>
        /// 查询条件
        /// </summary>
        /// <param name="conditionItems">查询条件项</param>
        /// <exception cref="NotSupportedException"></exception>
        public SQLCondition(IEnumerable<SQLConditionItem> conditionItems)
            : this(conditionItems.Select(item => item.AsGeneric<T>())) { }

        /// <summary>
        /// 查询条件
        /// </summary>
        /// <param name="conditionItems">查询条件项</param>
        public SQLCondition(IEnumerable<SQLConditionItem<T>> conditionItems)
        {
            if (conditionItems is null)
                return;

            foreach (var item in conditionItems)
            {
                if (item != null)
                {
                    Items.Add(item);
                }
            }
        }

        /// <summary>
        /// 转换条件值
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="keyValues">条件值</param>
        /// <returns></returns>
        private static IEnumerable<SQLConditionItem> GetConditionItems<TValue>(IEnumerable<KeyValuePair<string, TValue>> keyValues)
        {
            if (keyValues is null)
                yield break;

            foreach (var keyValue in keyValues)
            {
                yield return new SQLConditionItem
                {
                    MemberName = keyValue.Key,
                    Value = keyValue.Value
                };
            }
        }

        /// <summary>
        /// 配置忽略的条件
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="keySelector">属性选择</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public SQLCondition<T> IgnoreFor<TKey>(Expression<Func<T, TKey>> keySelector)
        {
            if (keySelector is null)
                throw new ArgumentNullException(nameof(keySelector));

            if (keySelector.Body.NodeType != ExpressionType.MemberAccess)
                throw new ArgumentException("要求表达式主体必须为MemberAccess表达式", nameof(keySelector));

            var exp = keySelector.Body as MemberExpression;
            // ReSharper disable once PossibleNullReferenceException
            var targets = Items.Where(item => item.Member == exp.Member).ToArray();

            foreach (var item in targets)
                Items.Remove(item);

            return this;
        }

        /// <summary>
        /// 配置比较操作符
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="keySelector">属性选择</param>
        /// <param name="operator">操作符</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public SQLCondition<T> OperatorFor<TKey>(Expression<Func<T, TKey>> keySelector, SQLOperator @operator)
        {
            if (keySelector is null)
                throw new ArgumentNullException(nameof(keySelector));

            if (keySelector.Body.NodeType != ExpressionType.MemberAccess)
                throw new ArgumentException("要求表达式主体必须为MemberAccess表达式", nameof(keySelector));

            var exp = keySelector.Body as MemberExpression;

            foreach (var item in Items)
            {
                // ReSharper disable once PossibleNullReferenceException
                if (item.Member == exp.Member)
                {
                    item.Operator = @operator;
                }
            }

            return this;
        }

        /// <summary>
        /// 转换为And连接的谓词筛选表达式
        /// </summary>
        /// <param name="trueWhenNull">当生成的表达式为null时返回true表达式</param>
        /// <returns></returns>
        public Expression<Func<T, bool>> ToAndPredicate(bool trueWhenNull = true)
        {
            if (Items.Count == 0)
                return trueWhenNull ? SQLPredicate.True<T>() : null;
            
            return Items
               .Select(item => item.ToPredicate())
               .Aggregate((left, right) => left.And(right));
        }

        /// <summary>
        /// 转换为Or连接的谓词筛选表达式
        /// </summary>
        /// <param name="falseWhenNull">当生成的表达式为null时返回false表达式</param>
        /// <returns></returns>
        public Expression<Func<T, bool>> ToOrPredicate(bool falseWhenNull = true)
        {
            if (Items.Count == 0)
                return falseWhenNull ? SQLPredicate.False<T>() : null;

            return Items
               .Select(item => item.ToPredicate())
               .Aggregate((left, right) => left.Or(right));
        }
    }
}