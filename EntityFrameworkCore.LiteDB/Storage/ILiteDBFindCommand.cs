using EntityFrameworkCore.LiteDB.Storage.Internal;
using LiteDB;
using System.Collections.Generic;

namespace EntityFrameworkCore.LiteDB.Storage
{
    public interface ILiteDBFindCommand
    {
        IEnumerable<BsonDocument> ExecuteFind(ILiteDBConnection connection);
    }
}
