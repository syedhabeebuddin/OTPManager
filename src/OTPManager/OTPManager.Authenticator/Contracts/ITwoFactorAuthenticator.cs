namespace OTPManager.Authenticator.Contracts
{
    public interface ITwoFactorAuthenticator
    {
        public string GenerateCode(string phoneNumber);
        public (bool isValid, long timeStepMatched) VerifyCode(string code, string phoneNumber, long previousTimeStamp);
    }
}
