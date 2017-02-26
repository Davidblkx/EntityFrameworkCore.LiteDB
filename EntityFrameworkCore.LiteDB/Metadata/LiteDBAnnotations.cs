using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Diagnostics;

namespace EntityFrameworkCore.LiteDB.Metadata
{
    public class LiteDBAnnotations
    {
        public LiteDBAnnotations(IAnnotatable metadata)
        {
            Metadata = metadata;
        }

        public virtual IAnnotatable Metadata { get; }

        public virtual object GetAnnotation(string annotationName)
            => annotationName == null ? null : Metadata[annotationName];

        public virtual bool SetAnnotation(string annotationName, object value)
        {
            var annotatable = Metadata as IMutableAnnotatable;
            Debug.Assert(annotatable != null);

            annotatable[annotationName] = value;
            return true;
        }

        public virtual bool CanSetAnnotation(string annotationName, object value)
            => true;
    }
}
