namespace OTPManager.Domain.Models
{
    public class VerifyCodeRequest
    {
        public string Code { get; set; }
        public string Phone { get; set; }
    }
}
