using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Basketball.WebService.DataTransferObjects
{
    [DataContract]
    public class LeagueStandingsDto
    {
        [DataMember]
        public List<DivisionStandingsDto> DivisionStandings { get; set; }

        public LeagueStandingsDto()
        {
            DivisionStandings = new List<DivisionStandingsDto>();
        }
    }
}