using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EntityFrameworkCore.LiteDB.Metadata.Internal
{
    public static class LiteDBInternalMetadataBuilderExtensions
    {
        public static LiteDBPropertyBuilderAnnotations LiteDB(this InternalPropertyBuilder builder,
                ConfigurationSource configurationSource)
            => new LiteDBPropertyBuilderAnnotations(builder, configurationSource, LiteDBFullAnnotationsNames.Instance);

        public static LiteDBEntityTypeBuilderAnnotations LiteDB(this InternalEntityTypeBuilder builder,
                ConfigurationSource configurationSource)
            => new LiteDBEntityTypeBuilderAnnotations(builder, configurationSource, LiteDBFullAnnotationsNames.Instance);
    }
}
