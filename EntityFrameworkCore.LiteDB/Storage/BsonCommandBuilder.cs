using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityFrameworkCore.LiteDB.Storage
{
    public class BsonCommandBuilder : IBsonCommandBuilder
    {
        private string _collectionName;
        private Type _collectionEntityType;

        private List<string> _fieldNames = new List<string>();


        public void AddCollection(string name, Type collectionEntityType)
        {
            _collectionName = name;
            _collectionEntityType = collectionEntityType;
        }

        public void AddField(string name)
        {
            _fieldNames.Add(name);
        }

        public ILiteDBFindCommand Build()
        {
            return new LiteDBFindCommand(_collectionName,  e => e.Keys.Intersect(_fieldNames).Count() == _fieldNames.Count );
        }
    }
}
