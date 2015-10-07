using System.Web.Caching;

namespace Basketball.Service
{
	// TODO Cache expiration? Or do I invalidate cache when changes are made?
	// http://msdn.microsoft.com/en-us/library/system.web.caching.cache.aspx
	// http://msdn.microsoft.com/en-us/library/18c1wd61.aspx
	// Stuff to cache? Current season, league table, team stats, news, events, all home page stuff, fixtures
	// Rules - when something that is cached changes it either needs to be updated or removed
	//		 - if something that could be cached isn't cached, get if from the database and cache it
	//		 - select, insert or update - add to cache
	//		 - delete - remove from cache
	// Problem? Update cache and exception thrown on commit? May get out of date cache
    public class CacheService : ICacheService
    {
		public property bool IsCacheEnabled
		{
			get
			{
				bool? isEnabled = (bool)ConfigurationManager.AppSettings["IsCacheEnabled"];
				
				return isEnabled ?? false;
			}
		}
	
		public bool TryGet<T>(CacheKey key, out T value);
		{
			if(!IsCacheEnabled)
				return false;
			
			value = Cache.Get(key);
			
			return value != null;
		}
		
		
		public void Add<T>(CacheKey key, T value)
		{
			Cache.Insert(key, value);
		}
		
		public void Remove(CacheKey key);
		{
			Cache.Remove(key);
		}
		
		/*bool Exists<T>(CacheKey key);
		{
		
		}*/
		
		// http://stackoverflow.com/questions/972307/can-you-loop-through-all-enum-values-c
		// TODO Add extension method
		public void ClearAll()
		{
			foreach(CacheKey key in Enum.GetValues(typeof(CacheKey)))
			{
				Remove(key);
			}
		}
	}
}