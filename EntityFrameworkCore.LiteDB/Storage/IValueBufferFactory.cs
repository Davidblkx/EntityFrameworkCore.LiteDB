using EntityFrameworkCore.LiteDB.Queries.Internal;
using LiteDB;
using Microsoft.EntityFrameworkCore.Storage;

namespace EntityFrameworkCore.LiteDB.Storage
{
    public interface IValueBufferFactory
    {
        ValueBuffer Create(BsonDocument recordData, IValueBufferFromBsonShaper valueBufferShaper);
    }
}
