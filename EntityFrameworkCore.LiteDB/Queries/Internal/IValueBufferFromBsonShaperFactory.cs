using EntityFrameworkCore.LiteDB.Queries.Expressions;

namespace EntityFrameworkCore.LiteDB.Queries.Internal
{
    public interface IValueBufferFromBsonShaperFactory
    {
        IValueBufferFromBsonShaper Create(FindExpression findExpression);
    }
}
