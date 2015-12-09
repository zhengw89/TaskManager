using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace TaskManager.Helper.CustomValidation
{
    public class IpValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return false;
            var reg = new Regex(@"((?:(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d)))\.){3}(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d))))");
            return reg.Match(value.ToString()).Success;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var reg = new Regex(@"((?:(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d)))\.){3}(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d))))");
                if (!reg.Match(value.ToString()).Success)
                {
                    return new ValidationResult("IP格式非法");
                }
            }

            return base.IsValid(value, validationContext);
        }
    }
}