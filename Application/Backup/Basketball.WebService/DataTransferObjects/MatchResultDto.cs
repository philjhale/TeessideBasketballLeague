using System.Collections.Generic;
using System.Runtime.Serialization;

using Basketball.Domain.Entities;

namespace Basketball.WebService.DataTransferObjects
{
    /// <summary>
    /// For reasons unknown dto classes must be marked as serialisable using DataContract/DataMember attributes.
    /// For some reason it is not required on entity classes
    /// </summary>
    [DataContract]
    public class MatchResultDto
    {
        [DataMember]
        public FixtureDto Fixture { get; set; }
        [DataMember]
        public List<PlayerFixtureDto> HomePlayerStats { get; set; }
        [DataMember]
        public List<PlayerFixtureDto> AwayPlayerStats { get; set; }

        public MatchResultDto(Fixture fixture, List<PlayerFixture> homePlayerStats, List<PlayerFixture> awayPlayerStats)
        {
            this.Fixture = new FixtureDto(fixture, true);

            this.HomePlayerStats = new List<PlayerFixtureDto>();
            homePlayerStats.ForEach(x => this.HomePlayerStats.Add(new PlayerFixtureDto(x)));

            this.AwayPlayerStats = new List<PlayerFixtureDto>();
            awayPlayerStats.ForEach(x => this.AwayPlayerStats.Add(new PlayerFixtureDto(x)));
        }
    }
}