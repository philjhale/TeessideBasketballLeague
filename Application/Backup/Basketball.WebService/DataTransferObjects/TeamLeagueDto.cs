using System.Runtime.Serialization;
using Basketball.Common.Mapping;
using Basketball.Domain.Entities;

namespace Basketball.WebService.DataTransferObjects
{
    [DataContract]
    public class TeamLeagueDto
    {
        [DataMember]
        public int TeamId { get; set; }
        [DataMember]
        public string TeamName { get; set; }
        [DataMember]
        public string GamesPlayed { get; set; }
        [DataMember]
        public string GamesWon { get; set; }
        [DataMember]
        public string GamesLost { get; set; }
        [DataMember]
        public string GamesPct { get; set; }
        [DataMember]
        public string PointsLeague { get; set; }

        public TeamLeagueDto(TeamLeague tl)
        {
            this.TeamId       = tl.Team.Id;
            this.TeamName     = tl.TeamName;
            this.GamesPlayed  = tl.GamesPlayed.ToString();
            this.GamesWon     = tl.GamesWonTotal.ToString();
            this.GamesLost    = tl.GamesLostTotal.ToString();
            this.GamesPct     = tl.GamesPct.ToString("0.00");
            this.PointsLeague = tl.PointsLeague.ToString();
        }
    }
}