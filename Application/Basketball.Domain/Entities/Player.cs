using System;
using Basketball.Common.Validation;
using Basketball.Common.Resources;
using System.ComponentModel.DataAnnotations;
using Basketball.Common.Domain;

namespace Basketball.Domain.Entities
{
    public class Player : Entity
    {
        public Player() {}

        public Player(string forename, string surname)
            : this()
        {
            Check.Require(!string.IsNullOrEmpty(forename) && forename.Trim() != string.Empty, "forename must be provided");
            Check.Require(!string.IsNullOrEmpty(surname) && surname.Trim() != string.Empty, "surname must be provided");

            Forename = forename;
            Surname = surname;
        }

        public Player(string forename, string surname, Team team)
            : this()
        {
            Check.Require(!string.IsNullOrEmpty(forename) && forename.Trim() != string.Empty, "forename must be provided");
            Check.Require(!string.IsNullOrEmpty(surname) && surname.Trim() != string.Empty, "surname must be provided");
            Check.Require(team != null, "team must be provided");

            Forename = forename;
            Surname = surname;
            Team = team;
        }

        public override string ToString()
        {
            return Forename + " " + Surname;
        }

        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        [StringLength(50, ErrorMessage = FormMessages.FieldTooLong)]
        public virtual string Forename { get; set; }

        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        [StringLength(50, ErrorMessage = FormMessages.FieldTooLong)]
        public virtual string Surname { get; set; }

        [StringLength(50, ErrorMessage = FormMessages.FieldTooLong)]
        public virtual string MiddleName { get; set; }

        public virtual Team Team { get; set; }

        public virtual DateTime? DOB { get; set; }

        // Readonly field
        public virtual int? Age
        {
            get { return (DOB.HasValue ? ((int?)(DateTime.Now - DOB.Value).TotalDays / 365) : null); }
        }

        [Integer]
        public virtual int? HeightFeet { get; set; }

        [Integer]
        public virtual int? HeightInches { get; set; }

        public virtual string ShortName { get { return Forename[0] + " " + Surname; } }

    }
}
