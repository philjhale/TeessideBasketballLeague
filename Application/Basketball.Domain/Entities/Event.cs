using System;
using System.ComponentModel.DataAnnotations;
using Basketball.Common.Domain;
using Basketball.Common.Resources;
using System.Web.Mvc;

namespace Basketball.Domain.Entities
{
    /// <summary>
    /// League related stuff that is happening. e.g. CVL, AGMs etc
    /// </summary>
    public class Event : Entity
    {
        public Event() { }

        public Event(string eventTitle, string eventDescription, DateTime eventDate)
            : this()
        {
            Check.Require(!string.IsNullOrEmpty(eventTitle) && eventTitle.Trim() != string.Empty,
                "eventName must be provided");
            Check.Require(!string.IsNullOrEmpty(eventDescription) && eventDescription.Trim() != string.Empty,
                "eventDescription must be provided");

            Title = eventTitle;
            Description = eventDescription;
            Date = eventDate;
        }

        // TODO Contact for event?

        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        [StringLength(30, ErrorMessage = FormMessages.FieldTooLong)]
        public string Title { get; set; }

        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        [StringLength(200, ErrorMessage = FormMessages.FieldTooLong)]
        public string Description { get; set; }

        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        public DateTime Date { get; set; }

        // Times
        [StringLength(5, ErrorMessage = FormMessages.FieldTooLong)]
        [RegularExpression(@"^$|^[0-2][0-9]:[0-5][0-9]$", ErrorMessage = FormMessages.FieldTwentyFourHour)]
        public string StartTime { get; set; }

        [StringLength(5, ErrorMessage = FormMessages.FieldTooLong)]
        [RegularExpression(@"^$|^[0-2][0-9]:[0-5][0-9]$", ErrorMessage = FormMessages.FieldTwentyFourHour)]
        public string EndTime { get; set; }

        [AllowHtml]
        [StringLength(3800, ErrorMessage = FormMessages.FieldTooLong)]
        public string Notes { get; set; }

        // Venue and address
        [StringLength(50, ErrorMessage = FormMessages.FieldTooLong)]
        public string Venue { get; set; }

        [StringLength(50, ErrorMessage = FormMessages.FieldTooLong)]
        public string AddressLine1 { get; set; }

        [StringLength(50, ErrorMessage = FormMessages.FieldTooLong)]
        public string AddressLine2 { get; set; }

        [StringLength(50, ErrorMessage = FormMessages.FieldTooLong)]
        public string AddressLine3 { get; set; }

        [StringLength(50, ErrorMessage = FormMessages.FieldTooLong)]
        public string AddressTown { get; set; }

        [StringLength(50, ErrorMessage = FormMessages.FieldTooLong)]
        public string AddressCounty { get; set; }

        [StringLength(9, ErrorMessage = FormMessages.FieldTooLong)]
        public string AddressPostCode { get; set; }
    }
}
