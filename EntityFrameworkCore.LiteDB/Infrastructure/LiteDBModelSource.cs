using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;

namespace EntityFrameworkCore.LiteDB.Infrastructure
{
    public class LiteDBModelSource : ModelSource
    {
        public LiteDBModelSource(IDbSetFinder setFinder, ICoreConventionSetBuilder coreConventionSetBuilder, 
            IModelCustomizer modelCustomizer, IModelCacheKeyFactory modelCacheKeyFactory) 
            : base(setFinder, coreConventionSetBuilder, modelCustomizer, modelCacheKeyFactory)
        { }


    }
}
