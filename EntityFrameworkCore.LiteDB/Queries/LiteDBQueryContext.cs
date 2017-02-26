using EntityFrameworkCore.LiteDB.Storage.Internal;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;

namespace EntityFrameworkCore.LiteDB.Queries
{
    public class LiteDBQueryContext : QueryContext
    {
        private readonly ILiteDBConnection _connection;

        public LiteDBQueryContext(Func<IQueryBuffer> queryBufferFactory,
                LazyRef<IStateManager> stateManager,
                IConcurrencyDetector concurrencyDetector,
                ILiteDBConnection connection)
            : base(queryBufferFactory, stateManager, concurrencyDetector)
        {
            _connection = connection;
        }

        public ILiteDBConnection Connection => _connection;
    }
}
