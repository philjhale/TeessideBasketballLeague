using System.ComponentModel.DataAnnotations;
using Basketball.Common.Resources;

namespace Basketball.Web.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        public string Username { get; set; }
    }
}