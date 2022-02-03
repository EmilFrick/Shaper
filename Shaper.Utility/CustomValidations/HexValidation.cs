using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaper.Utility.CustomValidations
{
    public class HexValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string strValue = value?.ToString();
            if (strValue is not null && SD.HexRx.IsMatch(strValue))
            {
                return null;
            }
            return new ValidationResult("You need to provide a valid Hex Value.");
        }
    }
}
