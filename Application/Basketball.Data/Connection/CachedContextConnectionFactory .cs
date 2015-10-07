using System.Configuration;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using EFCachingProvider;
using EFCachingProvider.Caching;

namespace Basketball.Data.Connection
{
    // http://www.codeproject.com/Articles/435142/Entity-Framework-Second-Level-Caching-with-DbConte
    // http://code.msdn.microsoft.com/EFProviderWrappers
    public class CachedContextConnectionFactory : IDbConnectionFactory
    {
        public DbConnection CreateConnection(string nameOrConnectionString)
        {
            // TODO Fix this. I couldn't figure out how to inject the parameter
            nameOrConnectionString = ConfigurationManager.ConnectionStrings["Basketball"].ConnectionString;

            var providerInvariantName = "System.Data.SqlClient";
 
            var wrappedConnectionString = "wrappedProvider=" +
                providerInvariantName + ";" +
                nameOrConnectionString; // Not using parameter because I couldn't work out how to inject it
 
            return new EFCachingConnection
            {
                ConnectionString = wrappedConnectionString,
                CachingPolicy = CachingPolicy.CacheAll,
                Cache = EntityCache.Instance
            };
        }
    }
}