using NUnit.Framework;
using Basketball.Domain.Entities;
using System;
using Basketball.Common;
using Basketball.Common.Domain;

namespace Basketball.Tests.Domain
{
    [TestFixture]
    public class CupTests
    {
        [Test]
        public void CanCreateCup()
        {
            string cupName = "Mega cup";

            Cup cup = new Cup(cupName);

            Assert.That(cup.CupName, Is.EqualTo(cupName));
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreateCupWithBlankCupName()
        {
            new Cup(" ");
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreateCupWithNullCupName()
        {
            new Cup(null);
        }
    }
}
