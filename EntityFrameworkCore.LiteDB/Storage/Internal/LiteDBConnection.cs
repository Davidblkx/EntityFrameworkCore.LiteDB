using System;
using System.Collections.Generic;
using LiteDB;
using System.Linq.Expressions;

namespace EntityFrameworkCore.LiteDB.Storage.Internal
{
    public class LiteDBConnection : ILiteDBConnection
    {
        public LiteDBConnection(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; set; }

        public LiteDatabase DbConnection { get; set; }

        public void Close()
        {
            try { DbConnection?.Dispose(); } catch { }
            DbConnection = null;
        }

        public IEnumerable<BsonDocument> ExecuteQuery(string collectionName, 
            Expression<Func<BsonDocument, bool>> predicate)
        {
            if (DbConnection == null)
                throw new Exception("Database connection is closed");

            if (!DbConnection.CollectionExists(collectionName)) return new List<BsonDocument>();

            return DbConnection.GetCollection(collectionName).Find(predicate);
        }

        public void Open()
        {
            if (DbConnection != null) return;
            DbConnection = new LiteDatabase(ConnectionString);
        }
    }
}
