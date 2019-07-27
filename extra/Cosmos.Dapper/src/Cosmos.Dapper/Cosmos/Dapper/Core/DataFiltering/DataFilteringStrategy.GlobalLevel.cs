using System;
using System.Linq.Expressions;
using Cosmos.Dapper.Core.DynamicQuery;
using Cosmos.Data.Statements;
using Cosmos.Domain.Core;

namespace Cosmos.Dapper.Core.DataFiltering
{
    public class GlobalLevelDataFilteringStrategy<TEntity> : IDataFilteringStrategy where TEntity : class, IEntity
    {
        public Type TypeOfEntity { get; }

        public Expression<Func<TEntity, bool>> PredicateExpression { get; }

        internal GlobalLevelDataFilteringStrategy(
            Expression<Func<TEntity, bool>> predicateExpression)
        {
            TypeOfEntity = typeof(TEntity);
            PredicateExpression = predicateExpression;
        }

        internal GlobalLevelDataFilteringStrategy(Func<Expression<Func<TEntity, bool>>> predicateFunc)
            : this(predicateFunc?.Invoke()) { }

        public ISQLPredicate GetFilteringPredicate()
        {
            return DynamicExpressionResolver.ResolveExprTree(PredicateExpression);
        }

        public (Type, Type) GetSignature() => (TypeOfEntity, TypeOfEntity);
    }
}