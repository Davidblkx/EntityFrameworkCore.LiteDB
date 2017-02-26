using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EntityFrameworkCore.LiteDB.Metadata.Internal
{
    public class LiteDBAnnotationsBuilder : LiteDBAnnotations
    {
        public LiteDBAnnotationsBuilder(InternalMetadataBuilder internalBuilder,
            ConfigurationSource configurationSource)
            : base(internalBuilder.Metadata)
        {
            MetadataBuilder = internalBuilder;
            ConfigurationSource = configurationSource;
        }

        public virtual ConfigurationSource ConfigurationSource { get; }

        public virtual InternalMetadataBuilder MetadataBuilder { get; }

        public override bool SetAnnotation(
            string annotationName,
            object value)
            => MetadataBuilder.HasAnnotation(annotationName, value, ConfigurationSource);

        public override bool CanSetAnnotation(
            string annotationName,
            object value)
            => MetadataBuilder.CanSetAnnotation(annotationName, value, ConfigurationSource);
    }
}
