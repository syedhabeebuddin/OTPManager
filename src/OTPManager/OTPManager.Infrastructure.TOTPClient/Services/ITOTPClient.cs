using OtpNet;

namespace OTPManager.Infrastructure.TOTPClient.Services
{
    public interface ITOTPClient
    {
        public Totp Client();
    }
}
