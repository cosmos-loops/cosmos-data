using System;
using System.Collections.Generic;
using System.Reflection;

namespace Cosmos.Dapper.Conventions
{
    public class ConventionConfig
    {
        private readonly List<Func<PropertyInfo, bool>> _propertyPredicates = new List<Func<PropertyInfo, bool>>();

        public IReadOnlyCollection<Func<PropertyInfo, bool>> PropertyPredicates => _propertyPredicates;

        public PropertyConventionStrategy PropertyConvention { get; private set; }

        public ConventionConfig Filter(Func<PropertyInfo, bool> predicate)
        {
            _propertyPredicates.Add(predicate);
            return this;
        }

        public void Configure(Action<PropertyConventionStrategy> configure)
        {
            var strategy = new PropertyConventionStrategy();
            PropertyConvention = strategy;
            configure?.Invoke(strategy);
        }
    }
}