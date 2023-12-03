namespace OTPManager.Application.Services
{
    public interface IOTPAuthenticator
    {
        public string GenerateCode(string phone);
        public bool VerifyCode(string code, string phone);
        public bool IsLimitExceeded(string phone);
    }
}
