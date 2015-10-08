using System.Collections.Generic;
using System.Runtime.Serialization;
using Basketball.Domain.Entities;
using System.Linq;

namespace Basketball.WebService.DataTransferObjects
{
    [DataContract]
    public class PlayerStatsDto
    {
        [DataMember]
        public Player Player { get; set; }
        [DataMember]
        public List<PlayerSeasonFixtureStatsDto> CurrentSeasonFixtureStats { get; set; }
        [DataMember]
        public List<PlayerCareerStatsDto> CareerStats { get; set; }

        public PlayerStatsDto(Player player, List<PlayerFixture> currentSeasonFixtureStats, List<Fixture> currentSeasonFixtures, List<PlayerSeasonStats> seasonStats, PlayerCareerStats careerStats)
        {
            this.Player = player;

            CurrentSeasonFixtureStats = new List<PlayerSeasonFixtureStatsDto>();
            foreach (var stat in currentSeasonFixtureStats)
            {
                CurrentSeasonFixtureStats.Add(new PlayerSeasonFixtureStatsDto(stat, currentSeasonFixtures.Where(x => x.Id == stat.Fixture.Id).Single()));
            }
            
            CareerStats = new List<PlayerCareerStatsDto>();
            foreach (var stat in seasonStats)
            {
                CareerStats.Add(new PlayerCareerStatsDto(stat));
            }

            CareerStats.Add(new PlayerCareerStatsDto(careerStats));
        }
    }
}