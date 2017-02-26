using Microsoft.EntityFrameworkCore.Metadata;

namespace EntityFrameworkCore.LiteDB.Metadata
{
    public interface ILiteDBAnnotationsProvider
    {
        ILiteDBEntityTypeAnnotations For(IEntityType entityType);
        ILiteDBKeyAnnotations For(IKey key);
        ILiteDBModelAnnotations For(IModel model);
        ILiteDBPropertyAnnotations For(IProperty property);
    }
}
