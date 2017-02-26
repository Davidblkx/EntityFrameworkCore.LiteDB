using EntityFrameworkCore.LiteDB.Queries.Expressions;

namespace EntityFrameworkCore.LiteDB.Queries.Internal
{
    public class ValueBufferFromBsonShaperFactory : IValueBufferFromBsonShaperFactory
    {
        public IValueBufferFromBsonShaper Create(FindExpression findExpression)
            => new ValueBufferFromBsonShaper(findExpression);
    }
}
