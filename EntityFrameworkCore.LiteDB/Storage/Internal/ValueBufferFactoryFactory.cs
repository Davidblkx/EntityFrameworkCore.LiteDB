using EntityFrameworkCore.LiteDB.Shared;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace EntityFrameworkCore.LiteDB.Storage.Internal
{
    public class ValueBufferFactoryFactory : IValueBufferFactoryFactory
    {
        private struct CacheKey
        {
            public CacheKey(IReadOnlyList<Type> valueTypes)
            {
                ValueTypes = valueTypes;
            }

            public IReadOnlyList<Type> ValueTypes { get; }

            private bool Equals(CacheKey other) => ValueTypes.SequenceEqual(other.ValueTypes);

            public override bool Equals(object obj)
                => !ReferenceEquals(null, obj) && obj is CacheKey && Equals((CacheKey)obj);

            public override int GetHashCode()
            {
                unchecked
                {
                    return ValueTypes.Aggregate(0, (t, v) => (t * 397) ^ v.GetHashCode());
                }
            }
        }

        private readonly ConcurrentDictionary<CacheKey, Action<object[]>> _cache
            = new ConcurrentDictionary<CacheKey, Action<object[]>>();

        public virtual IValueBufferFactory Create(
            IReadOnlyList<Type> valueTypes, IReadOnlyList<int> indexMap)
        {
            var processValuesAction = _cache.GetOrAdd(new CacheKey(valueTypes.ToArray()), _createValueProcessorDelegate);

            if (indexMap == null)
                return new ValueBufferFactory(processValuesAction);

            throw new NotImplementedException("Other scenarios are not yet implemented");
        }

        private static readonly Func<CacheKey, Action<object[]>> _createValueProcessorDelegate = CreateValueProcessor;

        private static Action<object[]> CreateValueProcessor(CacheKey cacheKey)
        {
            var valuesParam = Expression.Parameter(typeof(object[]), "values");

            var conversions = new List<Expression>();
            var valueTypes = cacheKey.ValueTypes;

            var valueVariable = Expression.Variable(typeof(object), "value");

            for (var i = 0; i < valueTypes.Count; i++)
            {
                var type = valueTypes[i];

                if (type.UnwrapNullableType().GetTypeInfo().IsEnum)
                {
                    var arrayAccess = Expression.ArrayAccess(valuesParam, Expression.Constant(i));

                    conversions.Add(Expression.Assign(valueVariable, arrayAccess));

                    conversions.Add(
                        Expression.Assign(
                            arrayAccess,
                            Expression.Convert(
                                Expression.Convert(
                                    valueVariable,
                                    type),
                                typeof(object))));
                }
            }

            if (conversions.Count == 0)
            {
                return null;
            }

            return Expression.Lambda<Action<object[]>>(
                Expression.Block(
                    new[] { valueVariable },
                    conversions),
                valuesParam)
                .Compile();
        }
    }
}
