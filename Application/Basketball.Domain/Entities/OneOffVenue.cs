using Basketball.Common.Resources;
using System.ComponentModel.DataAnnotations;
using Basketball.Common.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Basketball.Domain.Entities
{
    public class OneOffVenue : Entity
    {
        public OneOffVenue() { }

        public OneOffVenue(string venue)
            : this()
        {
            Check.Require(!string.IsNullOrEmpty(venue) && venue.Trim() != string.Empty,
                "venue must be provided");

            this.Venue = venue;
        }

        public string GetShortName()
        {
            return Venue;
        }

        public override string ToString()
        {
            return Venue;
        }

        public string GetFullAddress()
        {
            List<string> addressParts = new List<string>(new string[] { Venue, AddressLine1, AddressLine2, AddressLine3, AddressTown, AddressCounty, AddressPostCode });

            return string.Join(", ", addressParts.Where(x => !string.IsNullOrEmpty(x)).ToList());
        }

        [Required(ErrorMessage = FormMessages.FieldMandatory)]
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
    }
}
