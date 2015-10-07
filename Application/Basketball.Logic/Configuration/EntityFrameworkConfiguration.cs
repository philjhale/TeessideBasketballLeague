using System.Data.Entity;
using Basketball.Data;

namespace Basketball.Service.Configuration
{
    public class EntityFrameworkConfiguration
    {
        /// <summary>
        /// Disables EF migrations
        /// </summary>
        public static void SetInitializer()
        {
            Database.SetInitializer<BasketballContext>(null);
        }
    }
}