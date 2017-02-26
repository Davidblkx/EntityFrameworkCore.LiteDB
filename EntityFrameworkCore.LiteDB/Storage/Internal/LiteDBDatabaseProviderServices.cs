using EntityFrameworkCore.LiteDB.Infrastructure;
using EntityFrameworkCore.LiteDB.Metadata.Conventions.Internal;
using EntityFrameworkCore.LiteDB.Queries;
using EntityFrameworkCore.LiteDB.Queries.ExpressionVisitors;
using EntityFrameworkCore.LiteDB.ValueGeneration;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;

namespace EntityFrameworkCore.LiteDB.Storage.Internal
{
    public class LiteDBDatabaseProviderServices : DatabaseProviderServices
    {
        public LiteDBDatabaseProviderServices(IServiceProvider services)
            : base(services)
        {
        }

        public override IDatabaseCreator Creator
        {
            get { throw new NotImplementedException(); }
        }

        public override IDatabase Database
            => GetService<LiteDBDatabase>();

        public override IEntityQueryableExpressionVisitorFactory EntityQueryableExpressionVisitorFactory
            => GetService<LiteDBEntityQueryableExpressionVisitorFactory>();

        public override IEntityQueryModelVisitorFactory EntityQueryModelVisitorFactory
            => GetService<LiteDBEntityQueryModelVisitorFactory>();

        public override string InvariantName
        {
            get { throw new NotImplementedException(); }
        }

        public override IModelSource ModelSource
            => GetService<LiteDBModelSource>();

        public override IQueryContextFactory QueryContextFactory
            => GetService<LiteDBQueryContextFactory>();


        public override IDbContextTransactionManager TransactionManager
        {
            get { throw new NotImplementedException(); }
        }

        public override IValueGeneratorCache ValueGeneratorCache
            => GetService<LiteDBValueGeneratorCache>();

        public override IQueryCompilationContextFactory QueryCompilationContextFactory
            => GetService<LiteDBQueryCompilationContextFactory>();

        public override IConventionSetBuilder ConventionSetBuilder
            => GetService<LiteDBConventionSetBuilder>();
    }
}
