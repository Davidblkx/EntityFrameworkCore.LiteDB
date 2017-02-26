using EntityFrameworkCore.LiteDB.Storage;

namespace EntityFrameworkCore.LiteDB.Bson
{
    public interface IBsonQueryGenerator
    {
        ILiteDBFindCommand GenerateBsonCommand();

        IValueBufferFactory CreateValueBufferFactory(IValueBufferFactoryFactory valueBufferFactoryFactory);
    }
}
