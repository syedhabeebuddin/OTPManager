using Microsoft.Extensions.Options;
using OTPManager.Authenticator.Contracts;
using OTPManager.Domain.Configurations;
using OTPManager.Domain.Models;

namespace OTPManager.Application.Services
{
    public class OTPAuthenticator : IOTPAuthenticator
    {
        private readonly ITwoFactorAuthenticator _twoFactorAuthenticator;        
        private readonly ICacheManager _cacheManager;
        private readonly OTPManagerSettings _optManagerSettings;

        public OTPAuthenticator(
            ITwoFactorAuthenticator twoFactorAuthenticator
            , ICacheManager cacheManager
            , IOptions<OTPManagerSettings> optManagerSettings)
        {
            _twoFactorAuthenticator = twoFactorAuthenticator;
            _cacheManager = cacheManager;
            _optManagerSettings = optManagerSettings.Value;
        }
        public string GenerateCode(string phoneNumber)
        {
            var code = _twoFactorAuthenticator.GenerateCode(phoneNumber);
            var metaData = new OTPMetadata
            {
                Code = code,
                ExpirationTime = DateTime.UtcNow.AddSeconds(_optManagerSettings.CodeExpirationTimeInSeconds),
                GeneratedTime = DateTime.UtcNow,
                Phone = phoneNumber
            };

            _cacheManager.AddItem(phoneNumber, metaData);
            return code;
        }

        public bool VerifyCode(string phone, string code)
        {
            var item = _cacheManager.GetItem(phone, code);
            return item != null && item.IsValid(phone, code);
        }

        public bool IsLimitExceeded(string phone)
        {
            int limit = _optManagerSettings.AllowedConcurrentCodes;
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
