using System;
using System.Reflection;
using System.Linq.Expressions;
using Cosmos.Data.Statements;
using Cosmos.Domain.Core;
using Dapper.Internals;

namespace Cosmos.Dapper
{
    public static class DapperPredicates
    {
        public static ISQLFieldPredicate Field<TEntity>(
            Expression<Func<TEntity, object>> expression, SQLOperator op, object value, bool not = false)
            where TEntity : class, IEntity, new()
        {
            var property = ReflectionHelper.GetProperty(expression) as PropertyInfo;
            if (property == null)
                throw new ArgumentException("No property can be found by 'expression'", nameof(expression));

            return new SQLFieldPredicate<TEntity>
            {
                PropertyName = property.Name,
                Operator = op,
                Value = value,
                Not = not
            };
        }

        public static ISQLPropertyPredicate Property<TEntity, TEntity2>(
            Expression<Func<TEntity, object>> expression1, SQLOperator op,
            Expression<Func<TEntity2, object>> expression2, bool not = false)
            where TEntity : class, IEntity, new()
            where TEntity2 : class, IEntity, new()
        {
            var property1 = ReflectionHelper.GetProperty(expression1) as PropertyInfo;
            var property2 = ReflectionHelper.GetProperty(expression2) as PropertyInfo;
            if (property1 == null)
                throw new ArgumentException("No property can be found by 'expression1'", nameof(expression1));
            if (property2 == null)
                throw new ArgumentException("No property can be found by 'expression2'", nameof(expression2));
            return new SQLPropertyPredicate<TEntity, TEntity2>
            {
                PropertyName = property1.Name,
                PropertyName2 = property2.Name,
                Operator = op,
                Not = not
            };
        }

        public static ISQLPredicateGroup Group(SQLGroupOperator op, params ISQLPredicate[] predicates)
        {
            return new SQLPredicateGroup
            {
                Operator = op,
                Predicates = predicates
            };
        }

        public static ISQLExistsPredicate Exists<TSub>(ISQLPredicate predicate, bool not = false)
            where TSub : class
        {
            return new SQLExistsPredicate<TSub>
            {
                Not = not,
                Predicate = predicate
            };
        }

        public static ISQLBetweenPredicate Between<TEntity>(Expression<Func<TEntity, object>> expression, SQLBetweenValues values, bool not = false)
            where TEntity : class, IEntity, new()
        {
            var property = ReflectionHelper.GetProperty(expression) as PropertyInfo;
            if (property == null)
                throw new ArgumentException("No property can be found by 'expression'", nameof(expression));
            return new SQLBetweenPredicate<TEntity>
            {
                Not = not,
                PropertyName = property.Name,
                Value = values
            };
        }

        public static ISQLSort Sort<TEntity>(Expression<Func<TEntity>> expression, bool ascending = true)
        {
            var property = ReflectionHelper.GetProperty(expression) as PropertyInfo;
            if (property == null)
                throw new ArgumentException("No property can be found by 'expression'", nameof(expression));
            return new SQLSort(0, property.Name, ascending ? SQLSortType.ASC : SQLSortType.DESC);
        }
    }
}