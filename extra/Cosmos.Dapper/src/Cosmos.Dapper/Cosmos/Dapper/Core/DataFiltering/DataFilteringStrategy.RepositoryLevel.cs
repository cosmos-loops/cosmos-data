using System;
using System.Linq.Expressions;
using Cosmos.Dapper.Core.DynamicQuery;
using Cosmos.Data.Statements;
using Cosmos.Domain.Core;

namespace Cosmos.Dapper.Core.DataFiltering
{
    public class RepositoryLevelDataFilteringStrategy<TEntity> : IDataFilteringStrategy where TEntity : class, IEntity
    {
        public Type TypeOfRepository { get; }

        public Type TypeOfEntity { get; }

        public Expression<Func<TEntity, bool>> PredicateExpression { get; }

        internal RepositoryLevelDataFilteringStrategy(
            Type typeOfRepository,
            Expression<Func<TEntity, bool>> predicateExpression)
        {
            TypeOfRepository = typeOfRepository ?? throw new ArgumentNullException(nameof(typeOfRepository));
            TypeOfEntity = typeof(TEntity);
            PredicateExpression = predicateExpression;
        }

        public ISQLPredicate GetFilteringPredicate()
        {
            return DynamicExpressionResolver.ResolveExprTree(PredicateExpression);
        }

        public (Type, Type) GetSignature() => (TypeOfRepository, TypeOfEntity);
    }
}