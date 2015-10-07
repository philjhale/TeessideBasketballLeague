using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

// TODO This needs testing! Unit testing and manual testing
namespace Basketball.Common.Validation
{
    public sealed class IntegerAttribute : ValidationAttribute
    {
        private const string defaultErrorMessage = "Field must be a whole number";  

        //Override IsValid  
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)  
        {
            int intValue;

            if (value != null && !int.TryParse(value.ToString(), out intValue))
            {
                return new ValidationResult(defaultErrorMessage); 
            }

            //Default return - This means there were no validation error  
            return null;  
        }  
   
    }
}
