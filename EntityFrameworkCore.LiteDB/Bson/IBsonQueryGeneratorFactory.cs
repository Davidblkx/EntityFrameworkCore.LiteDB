using EntityFrameworkCore.LiteDB.Queries.Expressions;

namespace EntityFrameworkCore.LiteDB.Bson
{
    public interface IBsonQueryGeneratorFactory
    {
        IBsonQueryGenerator Create(FindExpression finExpression);
    }
}
