using Microsoft.Extensions.Caching.Memory;
using OTPManager.Authenticator.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var otps = _cache.Get<IList<OTPMetadata>>(phone);

            if (otps == null)
                otps = new List<OTPMetadata>();

            otps = DeleteInActiveCodes(otps);
            otps.Add(metadata);
            _cache.Set<IEnumerable<OTPMetadata>>(phone, otps);
        }

        public OTPMetadata GetItem(string phone, string code)
        {
            var otps = _cache.Get<IList<OTPMetadata>>(phone);

            if (otps == null)
                return null;

            otps = DeleteInActiveCodes(otps);
            return otps.Where(x => x.Phone.Equals(phone) && x.Code.Equals(code)).FirstOrDefault();
        }

        public IList<OTPMetadata> GetItems(string phone)
        {
            return _cache.Get<IList<OTPMetadata>>(phone);
        }

        private IList<OTPMetadata> DeleteInActiveCodes(IList<OTPMetadata> listOfCodes)
        {
            return listOfCodes.Where(x => !x.IsActive).ToList();
        }
    }
}
