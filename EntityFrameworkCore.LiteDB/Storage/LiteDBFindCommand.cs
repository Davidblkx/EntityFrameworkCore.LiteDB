using EntityFrameworkCore.LiteDB.Storage.Internal;
using LiteDB;
using System.Collections.Generic;
using System;
using System.Linq.Expressions;

namespace EntityFrameworkCore.LiteDB.Storage
{
    internal class LiteDBFindCommand : ILiteDBFindCommand
    {
        private readonly string _collectionName;
        private readonly Expression<Func<BsonDocument, bool>> _predicate;

        public LiteDBFindCommand(string collectionName, Expression<Func<BsonDocument, bool>> predicate)
        {
            _collectionName = collectionName;
            _predicate = predicate;
        }

        public IEnumerable<BsonDocument> ExecuteFind(ILiteDBConnection connection)
        {
            connection.Open();

            var result = connection.ExecuteQuery(_collectionName, _predicate);

            connection.Close();

            return result;
        }
    }
}