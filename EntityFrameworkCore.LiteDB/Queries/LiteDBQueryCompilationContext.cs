using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Logging;
using System;

namespace EntityFrameworkCore.LiteDB.Queries
{
    public class LiteDBQueryCompilationContext : QueryCompilationContext
    {
        private readonly IQueryMethodProvider _queryMethodProvider;

        public LiteDBQueryCompilationContext(IModel model,
            ILogger logger,
            IEntityQueryModelVisitorFactory entityQueryModelVisitorFactory,
            IRequiresMaterializationExpressionVisitorFactory requiresMaterializationExpressionVisitorFactory,
            ILinqOperatorProvider linqOperatorProvider,
            Type contextType,
            IQueryMethodProvider queryMethodProvider,
            bool trackQueryResults)
            : base(model, logger, entityQueryModelVisitorFactory, requiresMaterializationExpressionVisitorFactory, linqOperatorProvider, contextType, trackQueryResults)
        {
            _queryMethodProvider = queryMethodProvider;
        }

        public IQueryMethodProvider QueryMethodProvider => _queryMethodProvider;
    }
}