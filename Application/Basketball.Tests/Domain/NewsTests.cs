using NUnit.Framework;
using Basketball.Domain.Entities;
using System;
using Basketball.Common.Domain;

namespace Basketball.Tests.Domain
{
    [TestFixture]
    public class NewsTests
    {
        #region Creation Tests
        [Test]
        public void CanCreateNews()
        {
            string subject = "This is a news subject"; 
            string message = "News message goes    here";
            DateTime newsDate = DateTime.Now;

            News news = new News(subject, message);

            Assert.That(news.Subject, Is.EqualTo(subject));
            Assert.That(news.Message, Is.EqualTo(message));
            Assert.That(news.NewsDate.Date, Is.EqualTo(newsDate.Date));
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreateNewsWithBlankSubject()
        {
            new News("   ", "message");
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreateNewsWithNullSubject()
        {
            new News(null, "message");
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreateNewsWithBlankMessage()
        {
            new News("subject", "   ");
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreateNewsWithNullMessage()
        {
            new News("subject", null);
        }

        #endregion
    }
}
