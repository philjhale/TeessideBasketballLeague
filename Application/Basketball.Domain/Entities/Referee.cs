using Basketball.Common.Resources;
using System.ComponentModel.DataAnnotations;
using Basketball.Common.Domain;

namespace Basketball.Domain.Entities
{
    public class Referee : Entity
    {
        public Referee() { }

        public Referee(string forname, string surname)
        {
            Check.Require(!string.IsNullOrEmpty(forname) && forname.Trim() != string.Empty, "forname must be provided");
            Check.Require(!string.IsNullOrEmpty(surname) && surname.Trim() != string.Empty, "surname must be provided");

            this.Forename = forname;
            this.Surname = surname;
        }

        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        [StringLength(50, ErrorMessage = FormMessages.FieldTooLong)]
        public virtual string Forename { get; set; }

        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        [StringLength(50, ErrorMessage = FormMessages.FieldTooLong)]
        public virtual string Surname { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}", Forename, Surname);
        }
    }
}

