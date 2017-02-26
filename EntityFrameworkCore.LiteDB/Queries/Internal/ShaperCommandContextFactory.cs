using EntityFrameworkCore.LiteDB.Bson;
using EntityFrameworkCore.LiteDB.Storage;
using System;

namespace EntityFrameworkCore.LiteDB.Queries.Internal
{
    public class ShaperCommandContextFactory : IShaperCommandContextFactory
    {
        private readonly IValueBufferFactoryFactory _valueBufferFactoryFactory;

        public ShaperCommandContextFactory(IValueBufferFactoryFactory valueBufferFactoryFactory)
        {
            _valueBufferFactoryFactory = valueBufferFactoryFactory;
        }

        public ShaperCommandContext Create(Func<IBsonQueryGenerator> bsonQueryGeneratorFactory)
            => new ShaperCommandContext(_valueBufferFactoryFactory, bsonQueryGeneratorFactory);
    }
}
