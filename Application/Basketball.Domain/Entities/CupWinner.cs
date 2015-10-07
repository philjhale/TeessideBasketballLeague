using Basketball.Common.Resources;
using System.ComponentModel.DataAnnotations;
using Basketball.Common.Domain;

namespace Basketball.Domain.Entities
{
    public class CupWinner : Entity
    {
        public CupWinner() {}

        public CupWinner(Season season, Cup cup, Team team)
            : this()
        {
            Check.Require(season != null, "season must be provided");
            Check.Require(cup    != null, "cup must be provided");
            Check.Require(team   != null, "team must be provided");

            this.Season = season;
            this.Cup    = cup;
            this.Team   = team;
        }

        
        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        public virtual Season Season { get; set; }

        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        public virtual Cup Cup { get; set; }

        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        public virtual Team Team { get; set; }
    }
}
