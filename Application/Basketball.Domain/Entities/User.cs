using System.Xml.Serialization;
using Basketball.Common.Resources;
using System.ComponentModel.DataAnnotations;
using Basketball.Common.Domain;

namespace Basketball.Domain.Entities
{
    
    public class User : Entity
    {
        public User() { }

        //public User(string subject, string message, string userId)
        //    : this()
        //{
        //    // Check for required values
        //    Check.Require(!string.IsNullOrEmpty(subject) && subject.Trim() != string.Empty, 
        //        "subject must be provided");
        //    Check.Require(!string.IsNullOrEmpty(message) && message.Trim() != string.Empty,
        //        "message must be provided");

        //    Subject = subject;
        //    ErrorMessage = message;
        //    UserId = userId;
        //    UserDate = DateTime.Now;
        //}


        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        [StringLength(50, ErrorMessage = FormMessages.FieldTooLong)]
        public string UserName { get; set; }

        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        [StringLength(100, ErrorMessage = FormMessages.FieldTooLong)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual Team Team { get; set; }

        // Roles are in user class because it's easier. Nuff said
        public bool SiteAdmin { get; set; }
        public bool TeamAdmin { get; set; }
        public bool FixtureAdmin { get; set; }
    }
}

