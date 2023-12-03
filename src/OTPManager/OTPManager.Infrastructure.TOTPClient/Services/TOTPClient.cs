using OtpNet;

namespace OTPManager.Infrastructure.TOTPClient.Services
{
    public class TOTPClient : ITOTPClient
    {
        private const string base32Secret = "6L4OH6DDC4PLNQBA5422GM67KXRDIQQP";
        private readonly Totp totp;
        public TOTPClient()
        {
            var secret = Base32Encoding.ToBytes(base32Secret);
            totp = new Totp(secret);
        }

        public Totp Client()
        {
            return totp;
        }
    }
}
