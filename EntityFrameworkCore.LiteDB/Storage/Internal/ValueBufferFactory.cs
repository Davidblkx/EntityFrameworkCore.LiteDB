using EntityFrameworkCore.LiteDB.Queries.Internal;
using LiteDB;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq;

namespace EntityFrameworkCore.LiteDB.Storage.Internal
{
    public class ValueBufferFactory : IValueBufferFactory
    {
        private readonly Action<object[]> _processValuesAction;

        public ValueBufferFactory(Action<object[]> processValuesAction)
        {
            _processValuesAction = processValuesAction;
        }

        public virtual ValueBuffer Create(BsonDocument recordData, IValueBufferFromBsonShaper valueBufferShaper) /* object*/
        {
            var fieldCount = recordData.Count;

            if (fieldCount == 0)
            {
                return ValueBuffer.Empty;
            }

            var values = new object[fieldCount];

            _processValuesAction?.Invoke(values);

            for (var i = 0; i < fieldCount; i++)
            {
                values[i] = recordData.Values.ToList()[i];
            }

            var idx = 0;
            foreach (var bsonElement in recordData)
            {
                values[idx] = bsonElement;
                idx++;
            }

            return valueBufferShaper.Shape(new ValueBuffer(values));
        }
    }
}
