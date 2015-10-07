using Basketball.Domain.Entities;
using Basketball.Common.Extensions;
using NUnit.Framework;

namespace Basketball.Tests.Common.Extensions
{
    [TestFixture]
    public class TypeExtensionMethodsTests
    {
        [Test]
        public void GetNavigationProperties_OneMatchingProperty_ReturnsCorrectList()
        {
            var result = typeof(ClassWithOneMatchingPropertyCup).GetNavigationProperties();

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0], Is.EqualTo("Cup"));
        }

        [Test]
        public void GetNavigationProperties_ZeroMatchingProperites_ReturnsCorrectList()
        {
            var result = typeof(ClassWithZeroMatchingProperties).GetNavigationProperties();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void GetNavigationProperties_ClassWithTreeMatchingPropertiesCupTeamPlayer_ReturnsCorrectList()
        {
            var result = typeof(ClassWithTreeMatchingPropertiesCupTeamPlayer).GetNavigationProperties();

            Assert.That(result.Count, Is.EqualTo(3));
            Assert.Contains("Cup", result);
            Assert.Contains("Team", result);
            Assert.Contains("Player", result);
        }

        #region Test classes
        private class ClassWithOneMatchingPropertyCup
        {
            public virtual Cup Cup { get; set; }
        }

        private class ClassWithZeroMatchingProperties
        {
            private int stuff;
            public string Bananas { get; set; }

            public ClassWithZeroMatchingProperties(int stuff, string bananas)
            {
                this.stuff = stuff;
                this.Bananas = bananas;
            }
        }

        private class ClassWithTreeMatchingPropertiesCupTeamPlayer
        {
            public virtual Cup Cup { get; set; }
            public virtual Team Team { get; set; }
            public virtual Player Player { get; set; }
            private Penalty Penalty;
            public int Crappys;
            private float felix;

            public ClassWithTreeMatchingPropertiesCupTeamPlayer(Penalty penalty, int crappys, float felix, Cup cup, Team team, Player player)
            {
                this.Penalty = penalty;
                this.Crappys = crappys;
                this.felix = felix;
                this.Cup = cup;
                this.Team = team;
                this.Player = player;
            }
        }
        #endregion
    }
}
