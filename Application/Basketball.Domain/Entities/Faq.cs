using System;
using System.Web.Mvc;
using Basketball.Common.Resources;
using System.ComponentModel.DataAnnotations;
using Basketball.Common.Domain;

namespace Basketball.Domain.Entities
{
    public class Faq : Entity
    {
        public Faq() { }

        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        [StringLength(300, ErrorMessage = FormMessages.FieldTooLong)]
        public virtual string Title { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        [StringLength(3800, ErrorMessage = FormMessages.FieldTooLong)]
        public virtual string Text { get; set; }

        public virtual DateTime LastUpdated { get; set; }
    }
}

