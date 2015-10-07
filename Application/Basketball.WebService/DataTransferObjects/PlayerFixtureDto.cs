using System.Runtime.Serialization;
using Basketball.Common.Mapping;
using Basketball.Domain.Entities;

namespace Basketball.WebService.DataTransferObjects
{
    [DataContract]
    public class PlayerFixtureDto
    {
        [DataMember]
        public int FixtureId { get; set; }
        [DataMember]
        public int PlayerId { get; set; }
        [DataMember]
        public string Forename { get; set; }
        [DataMember]
        public string Surname { get; set; }
        [DataMember]
        public int Fouls { get; set; }
        [DataMember]
        public int Points { get; set; }
        [DataMember]
        public bool IsMvp { get; set; }

        public PlayerFixtureDto(PlayerFixture pf)
        {
            this.FixtureId     = pf.Fixture.Id;
            this.PlayerId      = pf.Player.Id;
            this.Forename      = pf.Player.Forename;
            this.Surname       = pf.Player.Surname;
            this.Fouls         = pf.Fouls;
            this.Points        = pf.PointsScored;
            this.IsMvp         = pf.IsMvp.YesNoToBool();
        }
    }
}