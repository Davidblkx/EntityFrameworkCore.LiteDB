using EntityFrameworkCore.LiteDB.Queries.Expressions;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq.Expressions;

namespace EntityFrameworkCore.LiteDB.Queries.ExpressionVisitors.Internal
{
    public interface IMaterializerFactory
    {
        Expression<Func<ValueBuffer, object>> CreateMaterializer(
            IEntityType entityType,
            FindExpression findExpression,
            Func<IProperty, FindExpression, int> projectionAdder);
    }
}
