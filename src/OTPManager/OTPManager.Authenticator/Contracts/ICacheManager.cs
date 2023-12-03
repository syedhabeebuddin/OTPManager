using OTPManager.Domain.Models;

namespace OTPManager.Authenticator.Contracts
{
    public interface ICacheManager
    {
        public void AddItem(string phone, OTPMetadata metadata);
        public OTPMetadata GetItem(string phone, string code);
        public IList<OTPMetadata> GetItems(string phone);
    }
}
