using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaper.Utility.CustomValidations
{
    public class ShapeValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
            {
                return new ValidationResult("You need to provide a valid shape");
            }

            string strValue = value?.ToString().ToLower();
            bool match = false;
            foreach (var shape in Enum.GetNames(typeof(SD.Shapes)))
            {
                if (strValue == shape.ToLower())
                {
                    match = true;
                }
            }
            if (!match)
            {
                return new ValidationResult("You need to provide a valid shape");
            }
            return null;
        }
    }
}
