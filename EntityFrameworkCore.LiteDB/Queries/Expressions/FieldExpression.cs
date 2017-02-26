using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Linq.Expressions;

namespace EntityFrameworkCore.LiteDB.Queries.Expressions
{
    public class FieldExpression : Expression
    {
        private readonly IProperty _property;
        private readonly CollectionExpression _collectionExpression;

        public FieldExpression(string name, IProperty property, CollectionExpression collectionExpression)
            : this(name, property.ClrType, collectionExpression)
        {
            _property = property;
        }

        public FieldExpression(string name, Type type, CollectionExpression collectionExpression)
        {
            Name = name;
            Type = type;
            _collectionExpression = collectionExpression;
        }

        public virtual CollectionExpression Collection => _collectionExpression;

        public virtual string CollectionName => _collectionExpression.Name;

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
        public virtual IProperty Property => _property;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword

        public virtual string Name { get; }

        public override ExpressionType NodeType => ExpressionType.Extension;

        public override Type Type { get; }

        protected override Expression VisitChildren(ExpressionVisitor visitor) => this;

        private bool Equals(FieldExpression other)
            => ((_property == null && other._property == null)
                || (_property != null && _property.Equals(other._property)))
               && Type == other.Type
               && _collectionExpression.Equals(other._collectionExpression);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return (obj.GetType() == GetType())
                   && Equals((FieldExpression)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_property.GetHashCode() * 397)
                       ^ _collectionExpression.GetHashCode();
            }
        }

        public override string ToString() => Name;
    }

}
