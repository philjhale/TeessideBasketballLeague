using System;
using System.Collections.Generic;
using System.Linq;
using Basketball.Data.Interfaces;
using Basketball.Domain.Entities;
using Basketball.Service.Interfaces;
using Basketball.Common.BaseTypes;

namespace Basketball.Service
{
    public partial class EventService : BaseService<Event>, IEventService
    {
        private readonly IOptionService optionService;

        public EventService(IEventRepository eventRepository,
            IOptionService optionService)
            : base(eventRepository)
        {
            this.eventRepository = eventRepository;
            this.optionService = optionService;
        }

        public List<Event> GetNext()
        {
            return eventRepository.GetTop(Int32.Parse(optionService.GetByName(Option.HOME_NUM_EVENTS)), f => f.Date > DateTime.Now, o => o.OrderBy(e => e.Date)).ToList();
        }

    }
}
