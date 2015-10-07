using System.ComponentModel.DataAnnotations;
using Basketball.Common.Resources;
using Basketball.Service.Exceptions;

namespace Basketball.Web.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        public string NewPasswordConfirm { get; set; }


        /// <exception cref="ChangePasswordNewPasswordsDoNotMatchException"></exception>
        public void Validate()
        {
            if (NewPassword != NewPasswordConfirm)
                throw new ChangePasswordNewPasswordsDoNotMatchException();
        }
    }
}