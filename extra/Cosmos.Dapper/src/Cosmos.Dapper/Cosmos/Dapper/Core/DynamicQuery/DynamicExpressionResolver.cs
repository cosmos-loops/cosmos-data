using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cosmos.Data.Statements;

namespace Cosmos.Dapper.Core.DynamicQuery
{
    public static class DynamicExpressionResolver
    {
        /// <summary>
        /// Resolve expression to sql predicate
        /// </summary>
        /// <param name="expression"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public static ISQLPredicate ResolveExprTree<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class
        {
            return expression == null ? null : ResolveExprTree<TEntity>((BinaryExpression) expression.Body);
        }

        private static ISQLPredicate ResolveExprTree<TEntity>(BinaryExpression body) where TEntity : class
        {
            ISQLPredicate predicate;

            if (body.NodeType != ExpressionType.AndAlso && body.NodeType != ExpressionType.OrElse)
            {
                var propertyName = GetPropertyName(body);
                var propertyValue = GetPropertyValue(body.Right);
                var @operator = GetSQLOperator(body.NodeType);

                var fPredicate = new SQLFieldPredicate<TEntity>
                {
                    EntityType = typeof(TEntity),
                    PropertyName = propertyName,
                    Value = propertyValue,
                    Operator = @operator.@operator,
                    Not = @operator.not
                };

                predicate = fPredicate;
            }
            else
            {
                var group = new SQLPredicateGroup
                {
                    Operator = GetSQLGroupOperator(body.NodeType),
                    Predicates = new List<ISQLPredicate>()
                };

                var lPredicate = ResolveExprTree<TEntity>((BinaryExpression) body.Left);
                var rPredicate = ResolveExprTree<TEntity>((BinaryExpression) body.Right);

                group.Predicates.Add(lPredicate);
                group.Predicates.Add(rPredicate);

                predicate = group;
            }

            return predicate;
        }

        private static object GetPropertyValue(Expression source)
        {
            if (source is ConstantExpression constantExpression)
                return constantExpression.Value;

            var evalExpr = Expression.Lambda<Func<object>>(Expression.Convert(source, typeof(object)));
            var evalFunc = evalExpr.Compile();
            var value = evalFunc();
            return value;
        }

        private static string GetPropertyName(BinaryExpression body)
        {
            var propertyName = body.Left.ToString().Split('.')[1];

            if (body.Left.NodeType == ExpressionType.Convert)
            {
                //hack to remove the trailing ')' when converting.
                propertyName = propertyName.Replace(")", string.Empty);
            }

            return propertyName;
        }

        // ReSharper disable once InconsistentNaming
        private static SQLGroupOperator GetSQLGroupOperator(ExpressionType nodeType)
        {
            return nodeType == ExpressionType.AndAlso
                ? SQLGroupOperator.AND
                : SQLGroupOperator.OR;
        }

        // ReSharper disable once InconsistentNaming
        private static (SQLOperator @operator, bool not) GetSQLOperator(ExpressionType type)
        {
            switch (type)
            {
                case ExpressionType.Equal:
                    return (SQLOperator.EQ, false);

                case ExpressionType.NotEqual:
                    return (SQLOperator.EQ, true);

                case ExpressionType.LessThan:
                    return (SQLOperator.LT, false);

                case ExpressionType.GreaterThan:
                    return (SQLOperator.GT, false);

                case ExpressionType.GreaterThanOrEqual:
                    return (SQLOperator.GE, false);

                case ExpressionType.LessThanOrEqual:
                    return (SQLOperator.LE, false);

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}