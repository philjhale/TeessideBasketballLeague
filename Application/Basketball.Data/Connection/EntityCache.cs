using EFCachingProvider.Web;

namespace Basketball.Data.Connection
{
    public class EntityCache
    {
        private static AspNetCache cacheInstance;
        private static object lockObject = new object();

        public static AspNetCache Instance
        {
            get
            {
                if (cacheInstance == null)
                {
                    lock (lockObject)
                    {
                        if (cacheInstance == null)
                            cacheInstance = new AspNetCache();
                    }
                }

                return cacheInstance;
            }
        }
    }
}