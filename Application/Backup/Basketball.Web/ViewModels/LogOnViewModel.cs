using System.ComponentModel.DataAnnotations;
using Basketball.Common.Resources;

namespace Basketball.Web.ViewModels
{
    public class LogOnViewModel
    {
        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        public string Password { get; set; }
    }
}
