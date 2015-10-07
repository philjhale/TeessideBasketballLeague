using NUnit.Framework;
using Basketball.Common.Domain;
using System;
using Basketball.Domain.Entities;

namespace Basketball.Tests.Domain
{
    [TestFixture]
    public class PlayerTests
    {
        [Test]
        public void CanCreatePlayer()
        {
            string firstname = "firstname"; 
            string surname = "surname";

            Player player = new Player(firstname, surname);

            Assert.IsNotNull(player);
            Assert.That(player.Forename, Is.EqualTo(firstname));
            Assert.That(player.Surname, Is.EqualTo(surname));
        }

        [Test]
        public void CanCreatePlayerWithTeam()
        {
            string firstname = "firstname";
            string surname = "surname";
            Team team = new Team("name", "longname");

            Player player = new Player(firstname, surname, team);

            Assert.IsNotNull(player);
            Assert.That(player.Forename, Is.EqualTo(firstname));
            Assert.That(player.Surname, Is.EqualTo(surname));
            Assert.That(player.Team, Is.EqualTo(team));
        }

        [Test]
        public void CanCreatePlayerWithAllFields()
        {
            string firstname = "firstname";
            string surname = "surname";
            Team team = new Team("name", "longname");
            DateTime dob = new DateTime(1981, 03, 01);
            int? heightInches = 4;
            int? heightFeet = 7;
            int calculatedAge = (int)((DateTime.Now.Date - dob).TotalHours / 24 / 365);

            Player player = new Player(firstname, surname, team);
            player.DOB = dob;
            player.HeightInches = heightInches;
            player.HeightFeet = heightFeet;

            Assert.IsNotNull(player);
            Assert.That(player.Forename, Is.EqualTo(firstname));
            Assert.That(player.Surname, Is.EqualTo(surname));
            Assert.That(player.Team, Is.EqualTo(team));
            Assert.That(player.DOB, Is.EqualTo(dob));
            Assert.That(player.Age, Is.EqualTo(calculatedAge));
            Assert.That(player.HeightFeet, Is.EqualTo(heightFeet));
            Assert.That(player.HeightInches, Is.EqualTo(heightInches));
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreatePlayerWithNullForename()
        {
            new Player(null, "surname");
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreatePlayerWithBlankForename()
        {
            new Player("", "surname");
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreatePlayerWithEmptyForename()
        {
            new Player(" ", "surname");
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreatePlayerWithNullSurname()
        {
            new Player("forename", null);
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreatePlayerWithBlankSurname()
        {
            new Player("forename", "");
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreatePlayerWithEmptySurname()
        {
            new Player("forename", "   ");
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreatePlayerWithNullTeam()
        {
            new Player("forename", "surname", null);
        }

        [Test]
        public void CanGetPlayerToString()
        {
            Player player = new Player("Phil", "Hale");

            Assert.NotNull(player);
            Assert.That(player.ToString(), Is.EqualTo("Phil Hale"));
        }

        [Test]
        public void CanGetPlayerShortName()
        {
            Player player = new Player("Phil", "Hale");

            Assert.NotNull(player);
            Assert.That(player.ShortName, Is.EqualTo("P Hale"));
        }

    }
}
