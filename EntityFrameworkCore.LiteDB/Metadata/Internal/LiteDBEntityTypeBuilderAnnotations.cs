using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EntityFrameworkCore.LiteDB.Metadata.Internal
{
    public class LiteDBEntityTypeBuilderAnnotations : LiteDBEntityTypeAnnotations
    {
        public LiteDBEntityTypeBuilderAnnotations(InternalEntityTypeBuilder internalBuilder,
            ConfigurationSource configurationSource,
            LiteDBFullAnnotationsNames providerFullAnnotationNames)
            : base(new LiteDBAnnotationsBuilder(
                            internalBuilder, 
                            configurationSource),
                  providerFullAnnotationNames)
        {
        }

        protected new virtual LiteDBAnnotationsBuilder Annotations
            => (LiteDBAnnotationsBuilder)base.Annotations;

        private InternalEntityTypeBuilder EntityTypeBuilder
            => ((EntityType)EntityType).Builder;

        public virtual bool HasName(string value)
            => SetCollectionName(value);
    }
}
