using EntityFrameworkCore.LiteDB.Storage.Internal;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query;

namespace EntityFrameworkCore.LiteDB.Queries
{
    public class LiteDBQueryContextFactory : QueryContextFactory
    {
        private readonly ILiteDBConnection _connection;

        public LiteDBQueryContextFactory(
            ICurrentDbContext currentContext,
            IConcurrencyDetector concurrencyDetector,
            ILiteDBConnection connection)
        : base(currentContext, concurrencyDetector)
        {
            _connection = connection;
        }

        public override QueryContext Create()
            => new LiteDBQueryContext(
                CreateQueryBuffer,
                StateManager,
                ConcurrencyDetector,
                _connection);
    }
}