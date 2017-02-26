using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EntityFrameworkCore.LiteDB.Queries.Expressions
{
    public class CollectionExpression : Expression
    {
        public CollectionExpression(string collectionName, Type entityType)
        {
            Name = collectionName;
            EntityType = entityType;
        }

        public virtual string Name { get; }
        public virtual Type EntityType { get; }
        public override string ToString() => Name;
    }
}
