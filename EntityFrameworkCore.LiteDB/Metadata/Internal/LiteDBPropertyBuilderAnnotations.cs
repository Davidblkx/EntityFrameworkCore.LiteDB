using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EntityFrameworkCore.LiteDB.Metadata.Internal
{
    public class LiteDBPropertyBuilderAnnotations : LiteDBPropertyAnnotations
    {
        public LiteDBPropertyBuilderAnnotations(
            InternalPropertyBuilder internalBuilder,
            ConfigurationSource configurationSource,
            LiteDBFullAnnotationsNames providerFullAnnotationNames)
            : base(new LiteDBAnnotationsBuilder(internalBuilder, configurationSource), providerFullAnnotationNames)
        {
        }

        protected new virtual LiteDBAnnotationsBuilder Annotations
            => (LiteDBAnnotationsBuilder)base.Annotations;

        private InternalPropertyBuilder PropertyBuilder
            => ((Property)Property).Builder;

        public virtual bool HasColumnName(string value)
            => SetFieldName(value);

        public virtual bool HasColumnType(string value)
            => SetFieldType(value);
    }
}
