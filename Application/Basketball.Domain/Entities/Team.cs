using Basketball.Common.Resources;
using System.ComponentModel.DataAnnotations;
using Basketball.Common.Domain;

namespace Basketball.Domain.Entities
{
    public class Team : Entity
    {
        public Team() { }

        public Team(string teamName, string teamNameLong)
            : this()
        {
            Check.Require(!string.IsNullOrEmpty(teamName) && teamName.Trim() != string.Empty,
                "teamName must be provided");
            Check.Require(!string.IsNullOrEmpty(teamNameLong) && teamNameLong.Trim() != string.Empty,
                "teamNameLong must be provided");

            this.TeamName = teamName;
            this.TeamNameLong = teamNameLong;
        }

        public override string ToString()
        {
            return TeamNameLong;
        }


        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        [StringLength(12, ErrorMessage = FormMessages.FieldTooLong)]
        public  string TeamName { get; set; }

        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        [StringLength(50, ErrorMessage = FormMessages.FieldTooLong)]
        public  string TeamNameLong { get; set; }

        [StringLength(20, ErrorMessage = FormMessages.FieldTooLong)]
        public  string StripColour1 { get; set; }

        [StringLength(20, ErrorMessage = FormMessages.FieldTooLong)]
        public  string StripColour2 { get; set; }

        [StringLength(50, ErrorMessage = FormMessages.FieldTooLong)]
        public  string Venue { get; set; }

        [StringLength(50, ErrorMessage = FormMessages.FieldTooLong)]
        public  string AddressLine1 { get; set; }

        [StringLength(50, ErrorMessage = FormMessages.FieldTooLong)]
        public  string AddressLine2 { get; set; }

        [StringLength(50, ErrorMessage = FormMessages.FieldTooLong)]
        public  string AddressLine3 { get; set; }

        [StringLength(50, ErrorMessage = FormMessages.FieldTooLong)]
        public  string AddressTown { get; set; }

        [StringLength(50, ErrorMessage = FormMessages.FieldTooLong)]
        public  string AddressCounty { get; set; }

        [StringLength(9, ErrorMessage = FormMessages.FieldTooLong)]
        public  string AddressPostCode { get; set; }

        [StringLength(5, ErrorMessage = FormMessages.FieldTooLong)]
        [RegularExpression(@"^$|^[0-2][0-9]:[0-5][0-9]$", ErrorMessage = FormMessages.FieldTwentyFourHour)]
        public  string TipOffTime { get; set; }

        [StringLength(50, ErrorMessage = FormMessages.FieldTooLong)]
        public  string TeamContact1Forename { get; set; }

        [StringLength(50, ErrorMessage = FormMessages.FieldTooLong)]
        public  string TeamContact1Surname { get; set; }

        [DataType(DataType.EmailAddress)]
        [StringLength(50, ErrorMessage = FormMessages.FieldTooLong)]
        public  string TeamContact1Email { get; set; }

        [StringLength(20, ErrorMessage = FormMessages.FieldTooLong)]
        public  string TeamContact1ContactNumber1 { get; set; }

        [StringLength(20, ErrorMessage = FormMessages.FieldTooLong)]
        public  string TeamContact1ContactNumber2 { get; set; }

        [RegularExpression("^$|^http://.*", ErrorMessage = FormMessages.FieldUrlInvalidFormat)]
        [StringLength(50, ErrorMessage = FormMessages.FieldTooLong)]
        public  string WebSiteUrl { get; set; }

        //[ForeignKey("GameDay")]
        //public int? GameDay_Id { get; set; }
        public virtual DayOfWeek GameDay { get; set; }

        // Not sure if this will be required
        //public  string Active { get; set; }
    }
}
