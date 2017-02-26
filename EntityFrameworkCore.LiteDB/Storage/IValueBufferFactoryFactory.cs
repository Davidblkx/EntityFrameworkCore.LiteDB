using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkCore.LiteDB.Storage
{
    public interface IValueBufferFactoryFactory
    {
        IValueBufferFactory Create(IReadOnlyList<Type> type, IReadOnlyList<int> indexMap);
    }
}
