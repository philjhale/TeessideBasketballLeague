using System;
using Basketball.Common.Resources;
using System.ComponentModel.DataAnnotations;
using Basketball.Common.Domain;

namespace Basketball.Domain.Entities
{
    public class League : Entity
    {
        public League() 
        {
        }

        public League(Season season, string leagueDescription, int divisionNo, int displayOrder)
            : this()
        {
            Check.Require(season != null, "non null season must be provided");
            //Check.Require(endYear.ToString().Length == 4, "endYear must have 4 characters. e.g. 2009");

            this.Season = season;
            this.LeagueDescription = leagueDescription;
            this.DivisionNo = divisionNo;
            this.DisplayOrder = displayOrder;
        }

        //[Required(ErrorMessage = FormMessages.FieldMandatory)]
        public virtual Season Season { get; set; }

        //[Required(ErrorMessage = FormMessages.FieldMandatory)]
        [StringLength(50, ErrorMessage = FormMessages.FieldTooLong)]
        public virtual string LeagueDescription { get; set; }

        // TODO Add error checking too fields below. I couldn't get them to work correctly. Seems to be a problem
        // with ints
        //[Required(ErrorMessage = FormMessages.FieldMandatory)]
        //[IsNumeric(ErrorMessage = FormMessages.FieldNumeric)]
        [Range(1, 9999, ErrorMessage = FormMessages.FieldMandatory)]
        public virtual int DivisionNo { get; set; }

        //[Required(ErrorMessage = FormMessages.FieldMandatory)]
        [Range(1, 9999, ErrorMessage = FormMessages.FieldMandatory)]
        //[IsNumeric(ErrorMessage = FormMessages.FieldNumeric)]
        public virtual int DisplayOrder { get; set; }

        /// <summary>
        /// Returns LeagueDescription "Division" DivisionNo or just LeagueDescription
        /// is DivisionNo has no value
        /// </summary>
        public override String ToString()
        {
            return (!string.IsNullOrEmpty(LeagueDescription.Trim()) ? LeagueDescription + " " : "") + "Division " + DivisionNo;
        }

    }
}
