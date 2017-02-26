using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EntityFrameworkCore.LiteDB.Storage.Internal
{
    public interface ILiteDBConnection
    {
        string ConnectionString { get; }

        LiteDatabase DbConnection { get; }

        void Open();
        void Close();

        IEnumerable<BsonDocument> ExecuteQuery(string collectionName, 
            Expression<Func<BsonDocument, bool>> predicate);
    }
}
