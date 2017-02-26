using EntityFrameworkCore.LiteDB.Bson;

namespace EntityFrameworkCore.LiteDB.Queries.Expressions
{
    public class FindExpressionFactory : IFindExpressionFactory
    {
        private readonly IBsonQueryGeneratorFactory _bsonQueryGeneratorFactory;

        public FindExpressionFactory(IBsonQueryGeneratorFactory bsonQueryGeneratorFactory)
        {
            _bsonQueryGeneratorFactory = bsonQueryGeneratorFactory;
        }

        public FindExpression Create()
            => new FindExpression(_bsonQueryGeneratorFactory);
    }
}
