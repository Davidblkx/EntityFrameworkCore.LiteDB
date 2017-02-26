using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;

namespace EntityFrameworkCore.LiteDB.Queries.ExpressionVisitors.Internal
{
    public interface IShaper<TEntity>
    {
        TEntity Shape(QueryContext queryContext, ValueBuffer valueBuffer);
    }
}
