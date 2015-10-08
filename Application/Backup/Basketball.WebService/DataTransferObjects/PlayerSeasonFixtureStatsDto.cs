using System;
using System.Runtime.Serialization;
using Basketball.Common.Mapping;
using Basketball.Domain.Entities;

namespace Basketball.WebService.DataTransferObjects
{
    [DataContract]
    public class PlayerSeasonFixtureStatsDto
    {
        [DataMember]
        public DateTime FixtureDate { get; set; }
        [DataMember]
        public string CurrentTeam { get; set; }
        [DataMember]
        public string OpponentName { get; set; }
        [DataMember]
        public bool IsMvp { get; set; }
        [DataMember]
        public string Fouls { get; set; }
        [DataMember]
        public string Points { get; set; }


        /// <param name="playerFixture"></param>
        /// <param name="fixture">Required because EF won't load all the required information to look up the opposing team in a list of PlayerFixtures</param>
        public PlayerSeasonFixtureStatsDto(PlayerFixture playerFixture, Fixture fixture)
        {
            this.FixtureDate = playerFixture.Fixture.FixtureDate;
            this.CurrentTeam = playerFixture.TeamLeague.TeamName;
            // Sucky code alert. Because of the WCF/EF lazy loading problems only entities one level deep
            // from PlayerFixture are loaded. I.e. Fixture is loaded but Fixture.HomeTeamLeague isn't.
            // However PlayerFixture.TeamLeague will always be populated which means either 
            // PlayerFixture.Fixture.HomeTeamLeague or PlayerFixture.Fixture.AwayTeamLeague is populated,
            // I'm not entirely sure why. So the opposing team will be null
            if(fixture.HomeTeamLeague.Team != null) // Very odd. This seems to work because a Player's team as already been loaded
                OpponentName = fixture.AwayTeamLeague.TeamName;
            else
               OpponentName = fixture.HomeTeamLeague.TeamName;

            this.IsMvp = playerFixture.IsMvp.YesNoToBool();
            this.Fouls = playerFixture.Fouls.ToString();
            this.Points = playerFixture.PointsScored.ToString();
        }
    }
}