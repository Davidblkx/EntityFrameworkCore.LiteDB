using EntityFrameworkCore.LiteDB.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EntityFrameworkCore.LiteDB.Metadata
{
    public class LiteDBAnnotationsProvider : ILiteDBAnnotationsProvider
    {
        public ILiteDBModelAnnotations For( IModel model)
            => new LiteDBModelAnnotations(model, LiteDBFullAnnotationsNames.Instance);

        public ILiteDBPropertyAnnotations For( IProperty property)
            => new LiteDBPropertyAnnotations(property, LiteDBFullAnnotationsNames.Instance);

        public ILiteDBKeyAnnotations For( IKey key)
            => new LiteDBKeyAnnotations(key, LiteDBFullAnnotationsNames.Instance);

        public ILiteDBEntityTypeAnnotations For( IEntityType entityType)
            => new LiteDBEntityTypeAnnotations(entityType, LiteDBFullAnnotationsNames.Instance);
    }
}
