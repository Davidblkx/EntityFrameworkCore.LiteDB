using EntityFrameworkCore.LiteDB.Attributes;
using EntityFrameworkCore.LiteDB.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EntityFrameworkCore.LiteDB.Metadata.Conventions.Internal
{
    public class LiteDBCollectionAttributeConvention : EntityTypeAttributeConvention<CollectionAttribute>
    {
        public override InternalEntityTypeBuilder Apply(InternalEntityTypeBuilder entityTypeBuilder, 
            CollectionAttribute attribute)
        {
            if (!string.IsNullOrWhiteSpace(attribute.Name))
            {
                entityTypeBuilder.LiteDB(ConfigurationSource.DataAnnotation).HasName(attribute.Name);
            }

            return entityTypeBuilder;
        }
    }
}
