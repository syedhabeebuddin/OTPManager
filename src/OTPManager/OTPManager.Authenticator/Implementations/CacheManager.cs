using Microsoft.Extensions.Caching.Memory;
using OTPManager.Authenticator.Contracts;
using OTPManager.Domain.Models;

namespace OTPManager.Authenticator.Implementations
{
    public class CacheManager : ICacheManager
    {
        private readonly IMemoryCache _cache;
        public CacheManager(IMemoryCache cache)
        {
            _cache = cache;
        }

        public void AddItem(string phone, OTPMetadata metadata)
        {
            var otps = GetActiveItems(phone);
            otps.Add(metadata);
            _cache.Set<IEnumerable<OTPMetadata>>(phone, otps);
        }

        public OTPMetadata GetItem(string phone, string code)
        {   
            var otps = GetActiveItems(phone);
            return otps.Where(x => x.Phone.Equals(phone) && x.Code.Equals(code)).FirstOrDefault();
        }

        public IList<OTPMetadata> GetItems(string phone)
        {            
            return GetActiveItems(phone);
        }

        private IList<OTPMetadata> DeleteInActiveCodes(IList<OTPMetadata> listOfCodes,string phone)
        {
            var otps = listOfCodes.Where(x => x.IsActive).ToList();
            _cache.Set<IEnumerable<OTPMetadata>>(phone, otps);
            return otps;
        }

        private IList<OTPMetadata> GetActiveItems(string phone)
        {
            var listOfCodes = _cache.Get<IList<OTPMetadata>>(phone);
            if(listOfCodes == null)
                return new List<OTPMetadata>();

            var otps = listOfCodes.Where(x => x.IsActive).ToList();
            _cache.Set<IEnumerable<OTPMetadata>>(phone, otps);
            return otps;
        }
    }
}
