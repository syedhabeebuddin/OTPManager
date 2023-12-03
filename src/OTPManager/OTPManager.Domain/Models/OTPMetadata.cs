namespace OTPManager.Domain.Models
{
    public class OTPMetadata
    {
        public string Phone { get; set; }
        public string Code { get; set; }
        public DateTime ExpirationTime { get; set; } //UTC

        public DateTime GeneratedTime { get; set; } //UTC

        public bool IsActive
        {
            get
            {
                return DateTime.UtcNow < ExpirationTime;
            }
        }

        public bool IsValid(string phone, string code)
        {
            if (phone == null || code == null)
                return false;

            if (phone.Equals(Phone) && code.Equals(Code) && DateTime.UtcNow <= ExpirationTime)
                return true;

            return false;
        }
    }
}
