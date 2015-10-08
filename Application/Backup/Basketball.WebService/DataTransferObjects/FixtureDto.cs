using System;
using System.Runtime.Serialization;
using Basketball.Common.Mapping;
using Basketball.Common.Util;
using Basketball.Domain.Entities;

namespace Basketball.WebService.DataTransferObjects
{
    [DataContract]
    public class FixtureDto
    {
        [DataMember]
        public int FixtureId { get; set; }
        [DataMember]
        public string HomeTeamName { get; set; }
        [DataMember]
        public string AwayTeamName { get; set; }
        [DataMember]
        public string HomeTeamScore { get; set; }
        [DataMember]
        public string AwayTeamScore { get; set; }
        [DataMember]
        public bool IsPlayed { get; set; }
        [DataMember]
        public bool IsCancelled { get; set; }
        [DataMember]
        public DateTime FixtureDate { get; set; }
        [DataMember]
        public string TipOffTime { get; set; }
        [DataMember]
        public string Report { get; set; }

        public FixtureDto(Fixture fixture, bool includeReport)
        {
            this.FixtureId     = fixture.Id;
            this.HomeTeamName  = fixture.HomeTeamLeague.TeamName;
            this.AwayTeamName  = fixture.AwayTeamLeague.TeamName;
            this.HomeTeamScore = fixture.HomeTeamScore.ToString();
            this.AwayTeamScore = fixture.AwayTeamScore.ToString();
            this.IsPlayed      = fixture.IsPlayed.YesNoToBool();
            this.IsCancelled   = fixture.IsCancelled.YesNoToBool();
            this.FixtureDate   = fixture.FixtureDate;

            if (!string.IsNullOrEmpty(fixture.TipOffTime))
                this.TipOffTime = fixture.TipOffTime;
            else if (fixture.HomeTeamLeague.Team != null) // I've not idea why but sometimes this returns null in which case just ignore it
                this.TipOffTime = fixture.HomeTeamLeague.Team.TipOffTime; 

            if (includeReport) this.Report = Html.ConvertToPlainText(fixture.Report);
        }
    }
}