using Basketball.Common.Resources;
using System.ComponentModel.DataAnnotations;
using Basketball.Common.Domain;

namespace Basketball.Domain.Entities
{
    /// <summary>
    /// Stored the relationship between cups and leagues.
    /// e.g. A handicap cup may include all leagues
    /// </summary>
    public class CupLeague : Entity
    {
        public CupLeague() {}

        public CupLeague(Cup cup, League league)
            : this()
        {
            Check.Require(cup != null, "cup must be provided");
            Check.Require(league != null, "league must be provided");

            League = league;
            Cup = cup;
        }

        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        public virtual Cup Cup { get; set; }

        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        public virtual League League { get; set; }
    }
}
