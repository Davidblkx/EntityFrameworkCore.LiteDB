using EntityFrameworkCore.LiteDB.Queries.Expressions;
using System.Linq.Expressions;

namespace EntityFrameworkCore.LiteDB.Queries.Bson
{
    public interface IBsonExpressionVisitor
    {
        Expression VisitField(FieldExpression fieldExpression);

        Expression VisitFind(FindExpression findExpression);

        Expression VisitCollection(CollectionExpression collectionExpression);
    }
}
