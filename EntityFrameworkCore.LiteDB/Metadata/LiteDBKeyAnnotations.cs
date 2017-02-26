using EntityFrameworkCore.LiteDB.Metadata.Internal;
using EntityFrameworkCore.LiteDB.Shared;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityFrameworkCore.LiteDB.Metadata
{
    internal class LiteDBKeyAnnotations : ILiteDBKeyAnnotations
    {
        private const string DefaultPrimaryKeyNamePrefix = "PK";
        private const string DefaultAlternateKeyNamePrefix = "AK";

        private readonly LiteDBFullAnnotationsNames _fullAnnotationNames;

        public LiteDBKeyAnnotations( IKey key,
             LiteDBFullAnnotationsNames fullAnnotationNames)
        {
            Annotations = new LiteDBAnnotations(key);
            _fullAnnotationNames = fullAnnotationNames;
        }

        private LiteDBAnnotations Annotations { get; }
        private IKey Key => (IKey)Annotations.Metadata;

        private ILiteDBEntityTypeAnnotations GetAnnotations( IEntityType entityType)
           => new LiteDBEntityTypeAnnotations(entityType, _fullAnnotationNames);

        private ILiteDBPropertyAnnotations GetAnnotations( IProperty property)
           => new LiteDBPropertyAnnotations(property, _fullAnnotationNames);

        public string Name
        {
            get
            {
                return (string)Annotations.GetAnnotation(LiteDBFullAnnotationsNames.Instance.Name)
                       ?? GetDefaultName();
            }
            set { SetName(value); }
        }

        private bool SetName( string value)
           => Annotations.SetAnnotation(
               _fullAnnotationNames?.Name, value);

        private string GetDefaultName()
        {
            return GetDefaultKeyName(
                GetAnnotations(Key.DeclaringEntityType).CollectionName,
                Key.IsPrimaryKey(),
                Key.Properties.Select(p => GetAnnotations(p).FieldName));
        }

        public static string GetDefaultKeyName(
             string tableName, bool primaryKey,  IEnumerable<string> propertyNames)
        {
            var builder = new StringBuilder();

            if (primaryKey)
            {
                builder
                    .Append(DefaultPrimaryKeyNamePrefix)
                    .Append("_")
                    .Append(tableName);
            }
            else
            {
                builder
                    .Append(DefaultAlternateKeyNamePrefix)
                    .Append("_")
                    .Append(tableName)
                    .Append("_")
                    .AppendJoin(propertyNames, "_");
            }

            return builder.ToString();
        }
    }
}