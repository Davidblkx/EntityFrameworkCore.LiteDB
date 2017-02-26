using Microsoft.EntityFrameworkCore.Storage;

namespace EntityFrameworkCore.LiteDB.Queries.Internal
{
    public interface IValueBufferFromBsonShaper
    {
        ValueBuffer Shape(ValueBuffer buffer);
    }
}
