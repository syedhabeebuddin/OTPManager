namespace OTPManager.Domain.Configurations
{
    public class OTPManagerSettings
    {
        public int AllowedConcurrentCodes { get; set; }
        public int CodeExpirationTimeInSeconds { get; set; }
    }
}
