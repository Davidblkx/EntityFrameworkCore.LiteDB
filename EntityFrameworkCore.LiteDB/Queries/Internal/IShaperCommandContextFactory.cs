using EntityFrameworkCore.LiteDB.Bson;
using System;

namespace EntityFrameworkCore.LiteDB.Queries.Internal
{
    public interface IShaperCommandContextFactory
    {
        ShaperCommandContext Create(Func<IBsonQueryGenerator> bsonQueryGeneratorFunc);
    }
}
