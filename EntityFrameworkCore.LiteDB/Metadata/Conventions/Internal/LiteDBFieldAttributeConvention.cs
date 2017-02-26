using EntityFrameworkCore.LiteDB.Attributes;
using EntityFrameworkCore.LiteDB.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection;

namespace EntityFrameworkCore.LiteDB.Metadata.Conventions.Internal
{
    public class LiteDBFieldAttributeConvention : PropertyAttributeConvention<FieldAttribute>
    {
        public override InternalPropertyBuilder Apply(InternalPropertyBuilder propertyBuilder, 
            FieldAttribute attribute, MemberInfo clrMember)
        {
            if (!string.IsNullOrWhiteSpace(attribute.Name))
            {
                propertyBuilder.LiteDB(ConfigurationSource.DataAnnotation).HasColumnName(attribute.Name);
            }

            if (!string.IsNullOrWhiteSpace(attribute.TypeName))
            {
                propertyBuilder.LiteDB(ConfigurationSource.DataAnnotation).HasColumnType(attribute.TypeName);
            }

            return propertyBuilder;
        }
    }
}
