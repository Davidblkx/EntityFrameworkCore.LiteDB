using System;

namespace EntityFrameworkCore.LiteDB.Queries.ExpressionVisitors.Internal
{
    public abstract class Shaper
    {
        public abstract Type Type { get; }
    }
}
