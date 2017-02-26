using EntityFrameworkCore.LiteDB.Metadata;
using EntityFrameworkCore.LiteDB.Queries.Expressions;
using EntityFrameworkCore.LiteDB.Queries.ExpressionVisitors.Internal;
using EntityFrameworkCore.LiteDB.Queries.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors;
using Remotion.Linq.Clauses;
using System.Linq.Expressions;

namespace EntityFrameworkCore.LiteDB.Queries.ExpressionVisitors
{
    public class LiteDBEntityQueryableExpressionVisitorFactory : IEntityQueryableExpressionVisitorFactory
    {
        private readonly IModel _model;
        private readonly ILiteDBAnnotationsProvider _annotationsProvider;
        private readonly IFindExpressionFactory _findExpressionFactory;
        private readonly IMaterializerFactory _materializerFactory;
        private readonly IShaperCommandContextFactory _shaperCommandContextFactory;
        private readonly IValueBufferFromBsonShaperFactory _valueBufferShaperFactory;

        public LiteDBEntityQueryableExpressionVisitorFactory(
                IModel model,
                ILiteDBAnnotationsProvider annotationsProvider,
                IFindExpressionFactory findExpressionFactory,
                IMaterializerFactory materializerFactory,
                IShaperCommandContextFactory shaperCommandContextFactory,
                IValueBufferFromBsonShaperFactory valueBufferShaperFactory
        )
        {
            _model = model;
            _annotationsProvider = annotationsProvider;
            _findExpressionFactory = findExpressionFactory;
            _materializerFactory = materializerFactory;
            _shaperCommandContextFactory = shaperCommandContextFactory;
            _valueBufferShaperFactory = valueBufferShaperFactory;
        }

        public ExpressionVisitor Create(
            EntityQueryModelVisitor entityQueryModelVisitor,
            IQuerySource querySource)
            => new LiteDBEntityQueryableExpressionVisitor(
                entityQueryModelVisitor,
                _model,
                _annotationsProvider,
                _findExpressionFactory,
                _materializerFactory,
                _shaperCommandContextFactory,
                _valueBufferShaperFactory);
    }
}
