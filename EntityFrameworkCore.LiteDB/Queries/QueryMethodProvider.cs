using EntityFrameworkCore.LiteDB.Queries.ExpressionVisitors.Internal;
using EntityFrameworkCore.LiteDB.Queries.Internal;
using Microsoft.EntityFrameworkCore.Query;
using System.Collections.Generic;
using System.Reflection;

namespace EntityFrameworkCore.LiteDB.Queries
{
    public class QueryMethodProvider : IQueryMethodProvider
    {
        public virtual MethodInfo ShapedQueryMethod => _shapedQueryMethodInfo;

        private static readonly MethodInfo _shapedQueryMethodInfo
            = typeof(QueryMethodProvider).GetTypeInfo()
                .GetDeclaredMethod(nameof(_ShapedQuery));
        
        private static IEnumerable<T> _ShapedQuery<T>(
            QueryContext queryContext,
            ShaperCommandContext shaperCommandContext,
            IShaper<T> shaper,
            IValueBufferFromBsonShaper valueBufferShaper)
        {
            foreach (var valueBuffer in new QueryingEnumerable((LiteDBQueryContext)queryContext, shaperCommandContext, valueBufferShaper))
            {
                yield return shaper.Shape(queryContext, valueBuffer);
            }
        }
    }
}
