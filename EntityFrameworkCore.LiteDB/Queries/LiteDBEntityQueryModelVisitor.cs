using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace EntityFrameworkCore.LiteDB.Queries
{
    public class LiteDBEntityQueryModelVisitor : EntityQueryModelVisitor
    {
        public LiteDBEntityQueryModelVisitor(
         IQueryOptimizer queryOptimizer,
         INavigationRewritingExpressionVisitorFactory navigationRewritingExpressionVisitorFactory,
         ISubQueryMemberPushDownExpressionVisitor subQueryMemberPushDownExpressionVisitor,
         IQuerySourceTracingExpressionVisitorFactory querySourceTracingExpressionVisitorFactory,
         IEntityResultFindingExpressionVisitorFactory entityResultFindingExpressionVisitorFactory,
         ITaskBlockingExpressionVisitor taskBlockingExpressionVisitor,
         IMemberAccessBindingExpressionVisitorFactory memberAccessBindingExpressionVisitorFactory,
         IOrderingExpressionVisitorFactory orderingExpressionVisitorFactory,
         IProjectionExpressionVisitorFactory projectionExpressionVisitorFactory,
         IEntityQueryableExpressionVisitorFactory entityQueryableExpressionVisitorFactory,
         IQueryAnnotationExtractor queryAnnotationExtractor,
         IResultOperatorHandler resultOperatorHandler,
         IEntityMaterializerSource entityMaterializerSource,
         IExpressionPrinter expressionPrinter,
         QueryCompilationContext queryCompilationContext)
        : base(
                queryOptimizer,
                navigationRewritingExpressionVisitorFactory,
                subQueryMemberPushDownExpressionVisitor,
                querySourceTracingExpressionVisitorFactory,
                entityResultFindingExpressionVisitorFactory,
                taskBlockingExpressionVisitor,
                memberAccessBindingExpressionVisitorFactory,
                orderingExpressionVisitorFactory,
                projectionExpressionVisitorFactory,
                entityQueryableExpressionVisitorFactory,
                queryAnnotationExtractor,
                resultOperatorHandler,
                entityMaterializerSource,
                expressionPrinter,
                queryCompilationContext)
        {
        }

        public virtual new LiteDBQueryCompilationContext QueryCompilationContext
            => (LiteDBQueryCompilationContext)base.QueryCompilationContext;
    }
}