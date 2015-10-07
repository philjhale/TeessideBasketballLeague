using System.Collections.Generic;
using Basketball.Domain.Entities;
using Basketball.Common.BaseTypes.Interfaces;

namespace Basketball.Service.Interfaces
{
    public partial interface IEventService : IBaseService<Event>
    {
        List<Event> GetNext();
    }
}
