using System.Collections.Generic;
using System.Runtime.Serialization;
using Basketball.Domain.Entities;

namespace Basketball.WebService.DataTransferObjects
{
    [DataContract]
    public class DivisionStandingsDto
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public List<TeamLeagueDto> Standings { get; set; }

        public DivisionStandingsDto(string name, List<TeamLeague> standings)
        {
            this.Name = name;
            
            Standings = new List<TeamLeagueDto>();
            standings.ForEach(x => this.Standings.Add(new TeamLeagueDto(x)));
        }
    }
}