using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Cosmos.Dapper.Core.Helpers
{
    internal static class LambdaHelper
    {
        public static PropertyInfo GetProperty<TSource, TProperty>(Expression<Func<TSource, TProperty>> propertyExpression)
        {
            var type = typeof(TSource);

            var member = propertyExpression.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException($"Expression '{propertyExpression}' refers to a method, not a property");

            var propertyInfo = member.Member as PropertyInfo;
            if (propertyInfo == null)
                throw new ArgumentException($"Expression '{propertyExpression}' refers to a field, not a property.");

            if (type != propertyInfo.ReflectedType && !type.IsSubclassOf(propertyInfo.ReflectedType))
                throw new ArgumentNullException($"Expression '{propertyExpression}' refers to a property that is not from type {type}");

            return propertyInfo;
        }
    }
}