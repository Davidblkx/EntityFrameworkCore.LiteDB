using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace EntityFrameworkCore.LiteDB.Queries.ExpressionVisitors.Internal
{
    public class BufferedEntityShaper<TEntity> : Shaper, IShaper<TEntity>
        where TEntity : class
    {
        public BufferedEntityShaper(string entityType, bool trackingQuery, IKey key,
            Func<ValueBuffer, object> materializer)
        {
            EntityType = entityType;
            IsTrackingQuery = trackingQuery;
            Key = key;
            Materializer = materializer;
        }

        protected virtual string EntityType { get; }

        protected virtual bool IsTrackingQuery { get; }

        protected virtual IKey Key { get; }

        protected virtual Func<ValueBuffer, object> Materializer { get; }

        public override Type Type => typeof(TEntity);

        public virtual TEntity Shape(QueryContext queryContext, ValueBuffer valueBuffer)
        {
            var entity
                = (TEntity)queryContext.QueryBuffer
                    .GetEntity(
                        Key,
                        new EntityLoadInfo(valueBuffer, Materializer),
                        queryStateManager: IsTrackingQuery,
                        throwOnNullKey: true);

            return entity;
        }

        public override string ToString() => "BufferedEntityShaper<" + typeof(TEntity).Name + ">";
    }
}
