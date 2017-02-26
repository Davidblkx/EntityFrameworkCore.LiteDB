using EntityFrameworkCore.LiteDB.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EntityFrameworkCore.LiteDB.Metadata
{
    public class LiteDBEntityTypeAnnotations : ILiteDBEntityTypeAnnotations
    {
        private readonly LiteDBFullAnnotationsNames FullAnnotationNames;

        public LiteDBEntityTypeAnnotations(IEntityType entityType, LiteDBFullAnnotationsNames fullAnnotationNames)
            : this(new LiteDBAnnotations(entityType), fullAnnotationNames)
        {
        }

        protected LiteDBEntityTypeAnnotations(LiteDBAnnotations annotations, 
            LiteDBFullAnnotationsNames fullAnnotationNames)
        {
            Annotations = annotations;
            FullAnnotationNames = fullAnnotationNames;
        }

        protected LiteDBAnnotations Annotations { get; }
        protected IEntityType EntityType => (IEntityType)Annotations.Metadata;

        protected LiteDBModelAnnotations GetAnnotations(IModel model)
            => new LiteDBModelAnnotations(model, FullAnnotationNames);

        protected LiteDBEntityTypeAnnotations GetAnnotations(IEntityType entityType)
             => new LiteDBEntityTypeAnnotations(entityType, FullAnnotationNames);

        public virtual string CollectionName
        {
            get
            {
                if (EntityType.BaseType != null)
                {
                    var rootType = EntityType.RootType();
                    return GetAnnotations(rootType).CollectionName;
                }

                return (string)Annotations.GetAnnotation(
                    LiteDBFullAnnotationsNames.Instance.CollectionName)
                       ?? EntityType.DisplayName();
            }
            set { SetCollectionName(value); }
        }

        protected bool SetCollectionName(string value)
            => Annotations.SetAnnotation(
                LiteDBFullAnnotationsNames.Instance.CollectionName, value);
    }
}
