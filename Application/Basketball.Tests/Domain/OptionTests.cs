using NUnit.Framework;
using Basketball.Domain.Entities;
using System;
using Basketball.Common.Domain;

namespace Basketball.Tests.Domain
{
    [TestFixture]
    public class OptionTests
    {
        [Test]
        public void CanCreateOption()
        {
            string optionName = "TEST123";
            string optionValue = "3";

            Option option = new Option(optionName, optionValue);

            Assert.That(option.Name, Is.EqualTo(optionName));
            Assert.That(option.Value, Is.EqualTo(optionValue));
        }

        [Test]
        public void CanCreateOptionBlankValue()
        {
            string optionName = "TEST123";
            string optionValue = " ";

            Option option = new Option(optionName, optionValue);

            Assert.That(option.Name, Is.EqualTo(optionName));
            Assert.That(option.Value, Is.EqualTo(optionValue));
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreateOptionWithBlankOptionName()
        {
            new Option(" ", "stuff");
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreateOptionWithNullOptionName()
        {
            new Option(null, "asd");
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreateOptionWithNullOptionValue()
        {
            new Option("NAME", null);
        }
    }
}
