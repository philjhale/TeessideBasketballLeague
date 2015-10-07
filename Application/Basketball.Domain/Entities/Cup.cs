using Basketball.Common.Resources;
using System.ComponentModel.DataAnnotations;
using Basketball.Common.Domain;

namespace Basketball.Domain.Entities
{
    public class Cup : Entity
    {
        public Cup() {}

        public Cup(string cupName)
            : this()
        {
            Check.Require(!string.IsNullOrEmpty(cupName) && cupName.Trim() != string.Empty, "cupName must be provided");

            CupName = cupName;
        }

        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        [StringLength(30, ErrorMessage = FormMessages.FieldTooLong)]
        public virtual string CupName { get; set; }

        public override string ToString()
        {
            return this.CupName;
        }
    }
}
