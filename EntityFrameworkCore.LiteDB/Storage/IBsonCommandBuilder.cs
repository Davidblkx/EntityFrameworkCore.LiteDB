using System;

namespace EntityFrameworkCore.LiteDB.Storage
{
    public interface IBsonCommandBuilder
    {
        void AddCollection(string name, Type collectionEntityType);

        void AddField(string name);

        ILiteDBFindCommand Build();
    }
}
