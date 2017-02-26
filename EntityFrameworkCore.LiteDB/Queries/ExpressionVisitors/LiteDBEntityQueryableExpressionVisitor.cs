using EntityFrameworkCore.LiteDB.Bson;
using EntityFrameworkCore.LiteDB.Metadata;
using EntityFrameworkCore.LiteDB.Queries.Expressions;
using EntityFrameworkCore.LiteDB.Queries.ExpressionVisitors.Internal;
using EntityFrameworkCore.LiteDB.Queries.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace EntityFrameworkCore.LiteDB.Queries.ExpressionVisitors
{
    public class LiteDBEntityQueryableExpressionVisitor : EntityQueryableExpressionVisitor
    {
        private readonly IModel _model;
        private readonly ILiteDBAnnotationsProvider _annotationsProvider;
        private readonly IFindExpressionFactory _findExpressionFactory;
        private readonly IMaterializerFactory _materializerFactory;
        private readonly IShaperCommandContextFactory _shaperCommandContextFactory;
        private readonly IValueBufferFromBsonShaperFactory _valueBufferShaperFactory;


        public LiteDBEntityQueryableExpressionVisitor(
             EntityQueryModelVisitor entityQueryModelVisitor,
             IModel model,
             ILiteDBAnnotationsProvider annotationsProvider,
             IFindExpressionFactory findExpressionFactory,
             IMaterializerFactory materializerFactory,
             IShaperCommandContextFactory shaperCommandContextFactory,
             IValueBufferFromBsonShaperFactory valueBufferShaperFactory)
            : base(entityQueryModelVisitor)
        {
            _model = model;
            _annotationsProvider = annotationsProvider;
            _findExpressionFactory = findExpressionFactory;
            _materializerFactory = materializerFactory;
            _shaperCommandContextFactory = shaperCommandContextFactory;
            _valueBufferShaperFactory = valueBufferShaperFactory;
        }

        private new LiteDBEntityQueryModelVisitor QueryModelVisitor => (LiteDBEntityQueryModelVisitor)base.QueryModelVisitor;

        protected override Expression VisitEntityQueryable(Type elementType)
        {
            var entityType = _model.FindEntityType(elementType.Name);
            var collectionName = _annotationsProvider.For(entityType).CollectionName;

            var findExpression = _findExpressionFactory.Create();

            findExpression.SetCollectionExpression(new CollectionExpression(collectionName, elementType));

            var shaper = CreateShaper(elementType, entityType, findExpression);

            Func<IBsonQueryGenerator> createQueryGenerator = findExpression.CreateBsonQueryGenerator;
            var valueBufferShaper = _valueBufferShaperFactory.Create(findExpression);

            return Expression.Call(
                QueryModelVisitor.QueryCompilationContext.QueryMethodProvider
                    .ShapedQueryMethod
                    .MakeGenericMethod(shaper.Type),
                EntityQueryModelVisitor.QueryContextParameter,
                Expression.Constant(_shaperCommandContextFactory.Create(createQueryGenerator)),
                Expression.Constant(shaper),
                Expression.Constant(valueBufferShaper));
        }

        private Shaper CreateShaper(Type elementType, IEntityType entityType, FindExpression findExpression)
        {
            Shaper shaper;

            var materializer
                = _materializerFactory
                    .CreateMaterializer(
                        entityType,
                        findExpression,
                        (p, se) =>
                            se.AddToProjection(
                                _annotationsProvider.For(p).FieldName,
                                p)
                        ).Compile();

            shaper
                = (Shaper)_createEntityShaperMethodInfo.MakeGenericMethod(elementType)
                    .Invoke(null, new object[]
                    {
                                entityType.DisplayName(),
                                QueryModelVisitor.QueryCompilationContext.IsTrackingQuery,
                                entityType.FindPrimaryKey(),
                                materializer
                    });

            return shaper;
        }

        private static readonly MethodInfo _createEntityShaperMethodInfo
           = typeof(LiteDBEntityQueryableExpressionVisitor).GetTypeInfo()
               .GetDeclaredMethod(nameof(CreateEntityShaper));
        
        private static IShaper<TEntity> CreateEntityShaper<TEntity>(
            string entityType,
            bool trackingQuery,
            IKey key,
            Func<ValueBuffer, object> materializer)
            where TEntity : class
            => new BufferedEntityShaper<TEntity>(
                    entityType,
                    trackingQuery,
                    key,
                    materializer);
    }
}
