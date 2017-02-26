using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;

namespace EntityFrameworkCore.LiteDB.Metadata.Conventions.Internal
{
    public class LiteDBConventionSetBuilder : IConventionSetBuilder
    {
        public virtual ConventionSet AddConventions(ConventionSet conventionSet)
        {
            var fieldAttributeConvention = new LiteDBFieldAttributeConvention();
            conventionSet.PropertyAddedConventions.Add(fieldAttributeConvention);
            conventionSet.PropertyFieldChangedConventions.Add(fieldAttributeConvention);

            var collectionAttributeConvention = new LiteDBCollectionAttributeConvention();
            conventionSet.EntityTypeAddedConventions.Add(collectionAttributeConvention);

            return conventionSet;
        }

        public static ConventionSet Build()
            => new LiteDBConventionSetBuilder()
                .AddConventions(new CoreConventionSetBuilder().CreateConventionSet());
    }
}
