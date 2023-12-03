using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace OTPManager.API.Validators
{
    public class CustomPhoneAttribute : ValidationAttribute
    {
        private const string regex = @"^(\d{10})$";
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var phone = value.ToString();
            if (Regex.IsMatch(phone, regex))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult($"Please enter valid phone number");
        }
    }
}
