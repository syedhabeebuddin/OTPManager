using OTPManager.Infrastructure.TOTPClient.Services;
using OtpNet;

namespace OTPManager.Authenticator.Implementations
{
    public class TwoFactorAuthenticator
    {
        private readonly ITOTPClient _totpClient;
        public TwoFactorAuthenticator(ITOTPClient totpClient)
        {
            _totpClient = totpClient;
        }
        public string GenerateCode(string phoneNumber)
        {
            //string base32Secret = "6L4OH6DDC4PLNQBA5422GM67KXRDIQQP";
            //var secret = Base32Encoding.ToBytes(base32Secret);

            //var totp = new Totp(secret);

            return _totpClient.Client().ComputeTotp();


        }

        public (bool isValid, long timeStepMatched) VerifyCode(string code, string phoneNumber, long previousTimeStamp)
        {
            //string base32Secret = "6L4OH6DDC4PLNQBA5422GM67KXRDIQQP";
            //var secret = Base32Encoding.ToBytes(base32Secret);

            //var totp = new Totp(secret);
            var isValid = _totpClient.Client().VerifyTotp(code, out long timeStepMatched,
                    VerificationWindow.RfcSpecifiedNetworkDelay);

            isValid &= previousTimeStamp != timeStepMatched;
            return (isValid, timeStepMatched);
        }
    }
}
