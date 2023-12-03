using OTPManager.Application.Configurations;
using OTPManager.Authenticator.Configurations;
using OTPManager.Infrastructure.TOTPClient.Configurations;

namespace OTPManager.API.Configurations
{
    public static class ServiceCollectionExtensions
    {
        public static void AddOTPManagerServices(this IServiceCollection services)
        {
            services.AddTOTPClientServices();            
            services.AddAuthenticatorServices();
            services.AddApplicationServices();
        }
    }
}
