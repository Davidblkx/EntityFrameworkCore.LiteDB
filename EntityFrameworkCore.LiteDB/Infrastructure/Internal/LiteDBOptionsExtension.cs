using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace EntityFrameworkCore.LiteDB.Infrastructure.Internal
{
    public class LiteDBOptionsExtension : IDbContextOptionsExtension
    {
        private string _connectionString;

        public virtual string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        public LiteDBOptionsExtension() { }

        public LiteDBOptionsExtension(LiteDBOptionsExtension copyFrom)
        {
            _connectionString = copyFrom.ConnectionString;
        }

        public void ApplyServices(IServiceCollection services)
        {
            services.AddEntityFrameworkLiteDB();
        }

        public static LiteDBOptionsExtension Extract(IDbContextOptions options)
        {
            var liteDbOptionsExtension = options.Extensions
                .OfType<LiteDBOptionsExtension>()
                .ToArray();

            if (liteDbOptionsExtension.Length == 0)
            {
                throw new InvalidOperationException("No LiteDB database providers are configured.");
            }

            if (liteDbOptionsExtension.Length > 1)
            {
                throw new InvalidOperationException("Multiple LiteDB database provider configurations found. A context can only be configured to use a single database provider.");
            }

            return liteDbOptionsExtension[0];
        }
    }
}
