using NUnit.Framework;
using Basketball.Domain.Entities;
using System;
using Basketball.Common;
using Basketball.Common.Domain;

namespace Basketball.Tests.Domain
{
    [TestFixture]
    public class EventTests
    {
        [Test]
        public void CanCreateEvent()
        {
            string eventTitle = "CVL";
            string eventDesc = "Junior CVL";
            DateTime eventDate = DateTime.Now;

            Event e = new Event(eventTitle, eventDesc, eventDate);

            Assert.That(e, Is.Not.Null);
            Assert.That(e.Title, Is.EqualTo(eventTitle));
            Assert.That(e.Description, Is.EqualTo(eventDesc));
            Assert.That(e.Date, Is.EqualTo(eventDate));
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreateEventWithBlankEventTitle()
        {
            new Event(" ", "Desc", DateTime.Now);
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreateEventWithNullEventTitle()
        {
            new Event(null, "Desc", DateTime.Now);
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreateEventWithBlankEventDesc()
        {
            new Event("Title", "", DateTime.Now);
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreateEventWithNullEventDesc()
        {
            new Event("Title", null, DateTime.Now);
        }
    }
}
