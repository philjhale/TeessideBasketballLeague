using System;
using System.Web.Mvc;
using Basketball.Common.Resources;
using System.ComponentModel.DataAnnotations;
using Basketball.Common.Domain;

namespace Basketball.Domain.Entities
{
    public class News : Entity
    {
        public News() 
        {
            NewsDate = DateTime.Now;
        }

        public News(string subject, string message)
            : this()
        {

            Check.Require(!string.IsNullOrEmpty(subject) && subject.Trim() != string.Empty, 
                "subject must be provided");
            Check.Require(!string.IsNullOrEmpty(message) && message.Trim() != string.Empty,
                "message must be provided");

            Subject = subject;
            Message = message;
            NewsDate = DateTime.Now;
        }

        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        [StringLength(50, ErrorMessage = FormMessages.FieldTooLong)]
        
        public virtual string Subject { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        [StringLength(3800, ErrorMessage = FormMessages.FieldTooLong)]
        
        public virtual string Message { get; set; }

        
        public virtual DateTime NewsDate { get; set; }
    }
}
