using EntityFrameworkCore.LiteDB.Metadata.Internal;
using EntityFrameworkCore.LiteDB.Shared;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Globalization;
using System.Reflection;

namespace EntityFrameworkCore.LiteDB.Metadata
{
    public class LiteDBPropertyAnnotations : ILiteDBPropertyAnnotations
    {
        protected readonly LiteDBFullAnnotationsNames _fullAnnotationNames;

        public LiteDBPropertyAnnotations(IProperty property,
            LiteDBFullAnnotationsNames providerFullAnnotationNames)
            : this(new LiteDBAnnotations(property), providerFullAnnotationNames)
        {
        }

        protected LiteDBPropertyAnnotations(LiteDBAnnotations annotations,
            LiteDBFullAnnotationsNames providerFullAnnotationNames)
        {
            Annotations = annotations;
            _fullAnnotationNames = providerFullAnnotationNames;
        }


        protected virtual LiteDBAnnotations Annotations { get; }
        protected virtual IProperty Property => (IProperty)Annotations.Metadata;

        public virtual string FieldName
        {
            get
            {
                return (string)Annotations.GetAnnotation(
                    _fullAnnotationNames?.FieldName)
                       ?? Property.Name;
            }
            set { SetFieldName(value); }
        }

        protected virtual bool SetFieldName(string value)
            => Annotations.SetAnnotation(_fullAnnotationNames?.FieldName, value);

        public virtual string FieldType
        {
            get
            {
                return (string)Annotations.GetAnnotation(_fullAnnotationNames?.FieldType);
            }
            set { SetFieldType(value); }
        }

        protected virtual bool SetFieldType(string value)
            => Annotations.SetAnnotation(_fullAnnotationNames?.FieldType, value);

        public virtual object DefaultValue
        {
            get
            {
                return (string)Annotations.GetAnnotation(
                    _fullAnnotationNames?.DefaultValue);
            }
            set { SetDefaultValue(value); }
        }

        protected virtual bool SetDefaultValue(object value)
        {
            if (value != null)
            {
                var valueType = value.GetType();
                if (Property.ClrType.UnwrapNullableType() != valueType)
                {
                    throw new InvalidOperationException(
                        $@"Cannot set default value '{value}' of type 
                           '{valueType}' on property '{Property.Name}'
                           of type '{Property.ClrType}' in entity type 
                           '{ Property.DeclaringEntityType.DisplayName() }'.");
                }

                if (valueType.GetTypeInfo().IsEnum)
                {
                    value = Convert.ChangeType(value, valueType.UnwrapEnumType(), CultureInfo.InvariantCulture);
                }
            }

            if (!CanSetDefaultValue(value))
            {
                return false;
            }

            if (DefaultValue != value
                && value != null)
            {
                SetDefaultValue(null);
            }

            return Annotations.SetAnnotation(
                _fullAnnotationNames?.DefaultValue,
                value);
        }

        protected virtual bool CanSetDefaultValue(object value)
        {
            if (!Annotations.CanSetAnnotation(
                _fullAnnotationNames?.DefaultValue,
                value))
            {
                return false;
            }

            return true;
        }
    }
}
