using EntityFrameworkCore.LiteDB.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EntityFrameworkCore.LiteDB.Metadata
{
    public class LiteDBModelAnnotations : ILiteDBModelAnnotations
    {
        private readonly LiteDBFullAnnotationsNames _fullAnnotationNames;

        public LiteDBModelAnnotations(IModel model, LiteDBFullAnnotationsNames fullAnnotationNames)
        {
            Annotations = new LiteDBAnnotations(model);
            _fullAnnotationNames = fullAnnotationNames;
        }

        private LiteDBAnnotations Annotations { get; }
        private IModel Model => (IModel)Annotations.Metadata;

        public string DatabaseName
        {
            get
            {
                return (string)Annotations.GetAnnotation(
                    LiteDBFullAnnotationsNames.Instance.DatabaseName);
            }
            set { SetDatabaseName(value); }
        }

        private bool SetDatabaseName(string value)
             => Annotations.SetAnnotation(_fullAnnotationNames?.DatabaseName, value);
    }
}