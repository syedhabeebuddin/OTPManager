namespace OTPManager.Authenticator.Contracts
{
    public interface ITwoFactorAuthenticator
    {
        public string GenerateCode(string phone);
        public (bool isValid, long timeStepMatched) VerifyCode(string code, string phone, long previousTimeStamp);
    }
}
