using EntityFrameworkCore.LiteDB.Bson;
using EntityFrameworkCore.LiteDB.Storage;
using System;

namespace EntityFrameworkCore.LiteDB.Queries.Internal
{
    public class ShaperCommandContext
    {
        private readonly IValueBufferFactoryFactory _valueBufferFactoryFactory;

        public ShaperCommandContext(IValueBufferFactoryFactory valueBufferFactoryFactory,
            Func<IBsonQueryGenerator> bsonQueryGeneratorFunc)
        {
            _valueBufferFactoryFactory = valueBufferFactoryFactory;
            BsonQueryGeneratorFunc = bsonQueryGeneratorFunc;
        }

        public virtual Func<IBsonQueryGenerator> BsonQueryGeneratorFunc { get; }

        private IValueBufferFactory _valueBufferFactory;
        public IValueBufferFactory ValueBufferFactory => _valueBufferFactory;

        public virtual ILiteDBFindCommand GetCommand()
        {
            ILiteDBFindCommand litedbCommand;

            var generator = BsonQueryGeneratorFunc();
            litedbCommand = generator.GenerateBsonCommand();

            return litedbCommand;
        }

        public virtual void NotifyReaderCreated()
            => _valueBufferFactory = BsonQueryGeneratorFunc()
                                    .CreateValueBufferFactory(_valueBufferFactoryFactory);
    }
}
