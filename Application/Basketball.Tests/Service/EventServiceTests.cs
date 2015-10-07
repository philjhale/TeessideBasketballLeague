using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Basketball.Service;
using Basketball.Domain.Entities;
using Rhino.Mocks;
using Basketball.Data;
using Basketball.Service.Interfaces;
using Basketball.Data.Interfaces;
using System.Data.Entity;
using Basketball.Tests.Data;
using Basketball.Common.BaseTypes.Interfaces;

namespace Basketball.Tests.Service
{
    [TestFixture]
    public class EventServiceTests
    {
        private MockRepository mock;
        private IEventRepository eventRepository;
        private IEventService eventService;
        private IDbContext mockContext;
        
        [SetUp]
        public void Setup()
        {
            mock = new MockRepository();
            mockContext = mock.PartialMock<TestBasketballContext>();
            // TODO This doesn't work
            eventRepository = mock.PartialMock<EventRepository>(mockContext);
            eventService = new EventService(eventRepository);
        }

        [TearDown]
        public void Teardown()
        {
            mock = null;
            eventRepository = null;
            eventService = null;
        }

        [Test]
        public void Get()
        {
            using (mock.Record())
            {
                Expect.Call(eventRepository.Get())
                    .IgnoreArguments()
                    .Return(GetEventList());
                //Expect.Call(delegate { eventRepository.Save(); });
                //Expect.Call(mockContext.Set<Event>())
                //    .Return(null);
            }
            
            List<Event> list = new List<Event>();
            using (mock.Playback())
            {
                list = eventService.Get();  
            }
            
 
            Assert.That(list, Is.Not.Null);
            Assert.That(list.Count, Is.EqualTo(2));
            Assert.That(list[0].Description, Is.EqualTo("description1"));
        }

        [Test]
        public void Insert()
        {
            Event newItem = new Event("s", "m", new DateTime());

            using (mock.Record())
            {
                Expect.Call(delegate { eventRepository.Insert(newItem); });
            }

            using (mock.Playback())
            {
                eventService.Insert(newItem);
            }
        }

        private IQueryable<Event> GetEventList()
        {
            List<Event> newEvent = new List<Event>();
            newEvent.Add(new Event("sub1", "description1", new DateTime()));
            newEvent.Add(new Event("sub2", "description2", new DateTime()));

            return newEvent.AsQueryable();
        }
    }
}
