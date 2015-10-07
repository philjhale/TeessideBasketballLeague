using System;
using System.ComponentModel.DataAnnotations.Schema;
using Basketball.Common.Resources;
using System.ComponentModel.DataAnnotations;
using Basketball.Common.Domain;

namespace Basketball.Domain.Entities
{
    public class Season : Entity
    {
        public Season() { }

        public Season(int startYear, int endYear)
            : this()
        {
            Check.Require(startYear.ToString().Length == 4, "startYear must have 4 characters. e.g. 2009");
            Check.Require(endYear.ToString().Length == 4, "endYear must have 4 characters. e.g. 2009");

            StartYear = startYear;
            EndYear = endYear;
        }

        public override String ToString() {
            return StartYear + "/" + EndYear;
        }

        // Purely a convenience method so season names can be obtained when generating lists
        [NotMapped]
        public string Name { get { return ToString(); } }

        
        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        //[StringLength(4, ErrorMessage = FormMessages.FieldTooLong)] Throws exception in EF
        public int StartYear { get; set; }

        
        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        //[StringLength(4, ErrorMessage = FormMessages.FieldTooLong)]
        public int EndYear { get; set; }
    }
}
