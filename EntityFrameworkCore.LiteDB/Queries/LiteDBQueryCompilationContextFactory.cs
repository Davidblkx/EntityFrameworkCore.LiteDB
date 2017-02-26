using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Remotion.Linq.Parsing.Structure.NodeTypeProviders;

namespace EntityFrameworkCore.LiteDB.Queries
{
    public class LiteDBQueryCompilationContextFactory : QueryCompilationContextFactory
    {
        public LiteDBQueryCompilationContextFactory(
            IModel model,
            ISensitiveDataLogger<LiteDBQueryCompilationContextFactory> logger,
            IEntityQueryModelVisitorFactory entityQueryModelVisitorFactory,
            IRequiresMaterializationExpressionVisitorFactory requiresMaterializationExpressionVisitorFactory,
            MethodInfoBasedNodeTypeRegistry methodInfoBasedNodeTypeRegistry,
            ICurrentDbContext currentContext)
            : base(model, logger,
                  entityQueryModelVisitorFactory,
                  requiresMaterializationExpressionVisitorFactory,
                  currentContext)
        {
        }

        public override QueryCompilationContext Create(bool async)
            => new LiteDBQueryCompilationContext(
                    Model,
                    (ISensitiveDataLogger)Logger,
                    EntityQueryModelVisitorFactory,
                    RequiresMaterializationExpressionVisitorFactory,
                    new LinqOperatorProvider(),
                    ContextType,
                    new QueryMethodProvider(),
                    TrackQueryResults);
    }
}