using EntityFrameworkCore.LiteDB.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace EntityFrameworkCore.LiteDB
{
    public static class LiteDBContextOptionsBuilderExtensions
    {
        public static DbContextOptionsBuilder UseLiteDB(
            this DbContextOptionsBuilder optionsBuilder,
            string connectionString,
            Action<DbContextOptionsBuilder> LiteDBOptionsAction = null)
        {
            var extension = GetOrCreateExtension(optionsBuilder);
            extension.ConnectionString = connectionString;

            ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(extension);

            LiteDBOptionsAction?.Invoke(new DbContextOptionsBuilder(optionsBuilder.Options));

            return optionsBuilder;
        }

        private static LiteDBOptionsExtension GetOrCreateExtension(DbContextOptionsBuilder options)
        {
            var existingExtension = options.Options.FindExtension<LiteDBOptionsExtension>();

            return existingExtension != null
                ? new LiteDBOptionsExtension(existingExtension)
                : new LiteDBOptionsExtension();
        }
    }
}
