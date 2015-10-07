using Basketball.Common.Domain;

namespace Basketball.Domain.Entities
{
    public class LeagueWinner : Entity
    {
        public LeagueWinner() {}

        public LeagueWinner(League league, Team team)
            : this()
        {
            Check.Require(league != null, "league must be provided");
            Check.Require(team != null, "team must be provided");

            this.League = league;
            this.Team = team;
        }

        
        //[Required(ErrorMessage = FormMessages.FieldMandatory)]
        public virtual League League { get; set; }

        //[Required(ErrorMessage = FormMessages.FieldMandatory)]
        public virtual Team Team { get; set; }
    }
}
