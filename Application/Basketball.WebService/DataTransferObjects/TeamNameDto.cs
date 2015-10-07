using System.Runtime.Serialization;

using Basketball.Domain.Entities;

namespace Basketball.WebService.DataTransferObjects
{
    [DataContract]
    public class TeamNameDto
    {
        [DataMember]
        public int TeamId { get; set; }
        [DataMember]
        public string TeamName { get; set; }

        public TeamNameDto(Team team)
        {
            this.TeamId   = team.Id;
            this.TeamName = team.TeamName;
        }
    }
}