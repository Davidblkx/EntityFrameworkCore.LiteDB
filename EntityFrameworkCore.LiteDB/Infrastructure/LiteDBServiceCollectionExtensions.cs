using EntityFrameworkCore.LiteDB.Bson;
using EntityFrameworkCore.LiteDB.Infrastructure.Internal;
using EntityFrameworkCore.LiteDB.Metadata;
using EntityFrameworkCore.LiteDB.Metadata.Conventions.Internal;
using EntityFrameworkCore.LiteDB.Queries;
using EntityFrameworkCore.LiteDB.Queries.Expressions;
using EntityFrameworkCore.LiteDB.Queries.ExpressionVisitors;
using EntityFrameworkCore.LiteDB.Queries.ExpressionVisitors.Internal;
using EntityFrameworkCore.LiteDB.Queries.Internal;
using EntityFrameworkCore.LiteDB.Storage;
using EntityFrameworkCore.LiteDB.Storage.Internal;
using EntityFrameworkCore.LiteDB.ValueGeneration;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace EntityFrameworkCore.LiteDB.Infrastructure
{
    public static class LiteDBServiceCollectionExtensions
    {
        public static IServiceCollection AddEntityFrameworkLiteDB(this IServiceCollection services)
        {
            services.TryAddEnumerable(ServiceDescriptor
                .Singleton<IDatabaseProvider, DatabaseProvider<LiteDBDatabaseProviderServices, LiteDBOptionsExtension>>());

            services.TryAdd(new ServiceCollection()
                .AddScoped<LiteDBConventionSetBuilder>()
                .AddScoped<LiteDBDatabaseProviderServices>()
                .AddScoped<ILiteDBConnection, LiteDBConnection>()
                .AddScoped<LiteDBQueryContextFactory>()
                .AddSingleton<LiteDBModelSource>()
                .AddScoped<LiteDBDatabase>()
                .AddScoped<LiteDBEntityQueryModelVisitorFactory>()
                .AddScoped<LiteDBEntityQueryableExpressionVisitorFactory>()
                .AddSingleton<ILiteDBAnnotationsProvider, LiteDBAnnotationsProvider>()
                .AddScoped<IFindExpressionFactory, FindExpressionFactory>()
                .AddScoped<IBsonQueryGeneratorFactory, BsonQueryGeneratorFactory>()
                .AddScoped<IMaterializerFactory, MaterializerFactory>()
                .AddScoped<IShaperCommandContextFactory, ShaperCommandContextFactory>()
                .AddScoped<LiteDBQueryCompilationContextFactory>()
                .AddScoped<IValueBufferFactoryFactory, ValueBufferFactoryFactory>()
                .AddSingleton<LiteDBValueGeneratorCache>()
                .AddScoped<IBsonCommandBuilderFactory, BsonCommandBuilderFactory>()
                .AddScoped<IValueBufferFromBsonShaperFactory, ValueBufferFromBsonShaperFactory>()
            );

            return services;
        }
    }
}
