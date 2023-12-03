using OTPManager.API.Validators;
using System.ComponentModel.DataAnnotations;

namespace OTPManager.API.Models
{
    public class VerifyCodeRequest
    {
        [Required]        
        [StringLength(6,MinimumLength = 6, ErrorMessage = "Only 6 digit codes are allowed.")]
        public string Code { get; set; }

        [CustomPhone]
        public string Phone { get; set; }
    }
}
