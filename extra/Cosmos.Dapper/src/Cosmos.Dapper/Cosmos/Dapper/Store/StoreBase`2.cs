using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using Cosmos.Dapper.Actions;
using Cosmos.Dapper.Core.Contextual;
using Cosmos.Data.Store;
using Cosmos.Disposables;
using Cosmos.Domain.Core;

namespace Cosmos.Dapper.Store
{
    public abstract partial class StoreBase<TContext, TEntity> : DisposableObjects, IStore<TEntity>
        where TContext : class, IDapperContext, IStoreContext, IWithSQLGenerator
        where TEntity : class, IEntity, new()
    {
        protected StoreBase(TContext context, Expression<Func<TContext, IDapperSet<TEntity>>> bindingExpression, bool includeUnsafeOpt)
        {
            EntityType = typeof(TEntity);

            RawTypedContext = context ?? throw new ArgumentNullException(nameof(context));

            BindingPropertyName = GetBindingPropertyName(bindingExpression);
            _dapperSet = context.DapperSetLazy<TEntity>(BindingPropertyName);

            var eap = LazyThreadSafetyMode.ExecutionAndPublication;
            _lazyEntityEntry = new Lazy<ISQLActionEntry<TEntity>>(() => RawTypedContext.GetActionEntry<TEntity>(), eap);
            _lazyAsynchronousEntityEntry = new Lazy<ISQLActionAsyncEntry<TEntity>>(() => RawTypedContext.GetAsynchronousActionEntry<TEntity>(), eap);

            IncludeUnsafeOpt = includeUnsafeOpt;
        }

        protected TContext RawTypedContext { get; }

        protected bool IncludeUnsafeOpt { get; }

        #region Binding property name

        static StoreBase()
        {
            _bindingPropertyCache = new ConcurrentDictionary<(Type, Type, int), PropertyInfo>();
        }

        // ReSharper disable once InconsistentNaming
        // ReSharper disable once StaticMemberInGenericType
        // key: typeOfContext, typeOfEntity and hashOfBindingExpression
        private static readonly ConcurrentDictionary<(Type, Type, int), PropertyInfo> _bindingPropertyCache;

        private static string GetBindingPropertyName(Expression<Func<TContext, IDapperSet<TEntity>>> bindingExpression)
        {
            bindingExpression.CheckNull(nameof(bindingExpression));
            var key = (typeof(TContext), typeof(TEntity), bindingExpression.GetHashCode());
            var result = _bindingPropertyCache.GetOrAdd(key, (tuple) =>
            {
                var propertyInfo = Lambdas.GetMember(bindingExpression) as PropertyInfo;
                if (propertyInfo == null)
                    throw new ArgumentException($"Cannot get property by expression '{bindingExpression}'");
                var typeOfPropertyInfoReflected = propertyInfo.ReflectedType;
                if (typeOfPropertyInfoReflected == null)
                    throw new ArgumentException($"Property '{propertyInfo.Name}' is not a property of '{tuple.Item1}'");
                if (tuple.Item1 != typeOfPropertyInfoReflected && !tuple.Item1.IsSubclassOf(typeOfPropertyInfoReflected))
                    throw new ArgumentException($"Expression '{bindingExpression}' refers to a property that is not from type {tuple.Item1}");
                var propertyType = propertyInfo.PropertyType;
                if (!propertyType.IsGenericType || propertyType.GenericTypeArguments.Length != 1)
                    throw new ArgumentException($"Such property type '{propertyType}' has no or more than one generic arguments.");
                var args1 = propertyType.GenericTypeArguments[0];
                if (args1 != tuple.Item2)
                    throw new ArgumentException($"Type '{args1}' is not same as entity type '{tuple.Item2}'");
                return propertyInfo;
            });

            if (result == null)
                throw new ArgumentException($"Cannot get property by expression '{bindingExpression}'");

            return result.Name;
        }

        #endregion

    }
}