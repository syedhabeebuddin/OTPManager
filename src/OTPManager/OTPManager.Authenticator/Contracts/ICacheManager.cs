using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTPManager.Authenticator.Contracts
{
    public interface ICacheManager
    {
        public void AddItem(string phone, OTPMetadata metadata);
        public OTPMetadata GetItem(string phone, string code);
        public IList<OTPMetadata> GetItems(string phone);
    }
}
