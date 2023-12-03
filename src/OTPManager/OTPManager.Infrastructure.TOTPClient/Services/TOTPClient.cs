using OtpNet;

namespace OTPManager.Infrastructure.TOTPClient.Services
{
    public class TOTPClient : ITOTPClient
    {        
        private const string base32Secret = "E6IMKF5WXYQH5OXYH7INKIDHCVYMIANK";
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
