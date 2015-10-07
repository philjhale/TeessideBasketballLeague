using Basketball.Common.Domain;

namespace Basketball.Common.Extensions
{
    public static class EntityExtensions
    {
        /// <summary>
        /// This method is mental. It purely reads something from the entity to ensure
        /// it's loaded. This is to be used before setting a navigation property
        /// to ensure it gets set rather than lazily loaded
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static void Touch(this Entity entity)
        {
            if(entity != null)
            {
                int i = entity.Id;
            }
        }

    }
}
