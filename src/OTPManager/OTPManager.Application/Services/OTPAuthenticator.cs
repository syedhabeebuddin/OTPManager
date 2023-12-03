using OTPManager.Authenticator.Contracts;
using OTPManager.Domain.Models;

namespace OTPManager.Application.Services
{
    public class OTPAuthenticator : IOTPAuthenticator
    {
        private readonly ITwoFactorAuthenticator _twoFactorAuthenticator;        
        private readonly ICacheManager _cacheManager;

        public OTPAuthenticator(
            ITwoFactorAuthenticator twoFactorAuthenticator
            , ICacheManager cacheManager)
        {
            _twoFactorAuthenticator = twoFactorAuthenticator;
            _cacheManager = cacheManager;
        }
        public string GenerateCode(string phoneNumber)
        {
            var code = _twoFactorAuthenticator.GenerateCode(phoneNumber);
            var metaData = new OTPMetadata
            {
                Code = code,
                ExpirationTime = DateTime.UtcNow.AddSeconds(30),
                GeneratedTime = DateTime.UtcNow,
                Phone = phoneNumber
            };

            _cacheManager.AddItem(phoneNumber, metaData);
            return code;
        }

        public bool VerifyCode(string code, string phoneNumber)
        {
            var item = _cacheManager.GetItem(phoneNumber, code);
            return item != null && item.IsValid(phoneNumber, code);
        }

        public bool IsLimitExceeded(string phone)
        {
            int limit = 2;
            var items = _cacheManager.GetItems(phone);
            return items != null && items.Count >= limit;
        }

        //public bool VerifyCode(string code, string phoneNumber)
        //{
        //    long previousTimeStamp = 10;
        //    var result = _twoFactorAuthenticator.VerifyCode(code, phoneNumber, previousTimeStamp);
        //    return result.isValid;
        //}
    }
}
