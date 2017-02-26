using EntityFrameworkCore.LiteDB.Queries.Expressions;
using EntityFrameworkCore.LiteDB.Storage;

namespace EntityFrameworkCore.LiteDB.Bson
{
    public class BsonQueryGeneratorFactory : IBsonQueryGeneratorFactory
    {
        private readonly IBsonCommandBuilderFactory _bsonCommandBuilderFactory;

        public BsonQueryGeneratorFactory(IBsonCommandBuilderFactory bsonCommandBuilderFactory)
        {
            _bsonCommandBuilderFactory = bsonCommandBuilderFactory;
        }

        public IBsonQueryGenerator Create(FindExpression findExpression)
            => new BsonQueryGenerator(_bsonCommandBuilderFactory, findExpression);
    }
}
