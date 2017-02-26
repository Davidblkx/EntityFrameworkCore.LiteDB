

namespace EntityFrameworkCore.LiteDB.Metadata
{
    public interface ILiteDBPropertyAnnotations
    {
        string FieldName { get; }
        string FieldType { get; }
        object DefaultValue { get; }
    }
}
