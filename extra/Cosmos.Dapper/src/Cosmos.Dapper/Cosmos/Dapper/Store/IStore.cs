using Cosmos.Domain.Core;

namespace Cosmos.Dapper.Store
{
    // ReSharper disable once PossibleInterfaceMemberAmbiguity
    public interface IStore<TEntity> :
        IQueryableStore<TEntity>,
        IDynamicExpressionQueryableStore<TEntity>,
        IPredicateQueryableStore<TEntity>,
        ISqlKataQueryableStore<TEntity>,
        IWriteableStore<TEntity>,
        IDynamicExpressionWriteableStore<TEntity>,
        ILightExpressionWriteableStore<TEntity>,
        IPredicateWriteableStore<TEntity>
        where TEntity : class, IEntity, new() { }

    // ReSharper disable RedundantExtendsListEntry
    public interface IStore<TEntity, in TKey> :
        IStore<TEntity>,
        IQueryableStore<TEntity, TKey>,
        IDynamicExpressionQueryableStore<TEntity>,
        IPredicateQueryableStore<TEntity>,
        ISqlKataQueryableStore<TEntity>,
        IWriteableStore<TEntity>,
        IDynamicExpressionWriteableStore<TEntity>,
        ILightExpressionWriteableStore<TEntity>,
        IPredicateWriteableStore<TEntity>
        where TEntity : class, IEntity<TKey>, new() { }
}