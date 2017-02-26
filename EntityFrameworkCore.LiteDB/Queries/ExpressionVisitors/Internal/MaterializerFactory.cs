using EntityFrameworkCore.LiteDB.Queries.Expressions;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq.Expressions;

namespace EntityFrameworkCore.LiteDB.Queries.ExpressionVisitors.Internal
{
    public class MaterializerFactory : IMaterializerFactory
    {
        private readonly IEntityMaterializerSource _entityMaterializerSource;

        public MaterializerFactory(IEntityMaterializerSource entityMaterializerSource)
        {
            _entityMaterializerSource = entityMaterializerSource;
        }

        public virtual Expression<Func<ValueBuffer, object>> CreateMaterializer(
            IEntityType entityType,
            FindExpression findExpression,
            Func<IProperty, FindExpression, int> projectionAdder)
        {
            var valueBufferParameter
                = Expression.Parameter(typeof(ValueBuffer), "valueBuffer");

            var indexMap = new int[entityType.PropertyCount()];
            var propertyIndex = 0;

            foreach (var property in entityType.GetProperties())
            {
                indexMap[propertyIndex++]
                    = projectionAdder(property, findExpression);
            }

            var materializer
                = _entityMaterializerSource
                    .CreateMaterializeExpression(
                        entityType, valueBufferParameter, indexMap);

            return Expression.Lambda<Func<ValueBuffer, object>>(materializer, valueBufferParameter);
        }
    }
}
