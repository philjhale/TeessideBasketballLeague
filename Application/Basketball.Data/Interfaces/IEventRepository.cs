using System.Linq;
using Basketball.Domain.Entities;
using Basketball.Common.BaseTypes.Interfaces;

namespace Basketball.Data.Interfaces
{
    public partial interface IEventRepository : IBaseRepository<Event>
    {
        IQueryable<Event> GetTopInFuture(int numResults);
    }
}
