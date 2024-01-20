
using API.Config;
using Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;


namespace API.Context
{
    public class DbContextFactory : IDbContextFactory, IDisposable
    {
        /// <summary>
        /// Create Db context with connection string
        /// </summary>
        /// <param name="settings"></param>
        public DbContextFactory(IOptions<DbContextSettings> settings) 
        {
            var options = new DbContextOptionsBuilder<DsEcommerceDbContext>().UseNpgsql(settings.Value.DbConnectionString).EnableSensitiveDataLogging().Options;
                //,
                //npgsqlOptionsAction: s =>
                //{
                //  s.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: null);
                //}).Options;
            DbContext = new DsEcommerceDbContext(options);
            DbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        /// <summary>
        /// Call Dispose to release DbContext
        /// </summary>
        ~DbContextFactory()
        {
            Dispose();
        }

        public DsEcommerceDbContext DbContext { get; private set; }
        /// <summary>
        /// Release DB context
        /// </summary>
        public void Dispose()
        {
            DbContext?.Dispose();
        }
    }
}
