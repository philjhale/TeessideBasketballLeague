using System.ComponentModel.DataAnnotations;

namespace Basketball.Common.Domain
{
    /*
     * Note: all navigation properties must be virtual to allow lazy loading
     */
    public abstract class Entity : IEntity
    {
        // TODO Should set be protected?
        [Key]
        public int Id { get; set; }
    }
}
