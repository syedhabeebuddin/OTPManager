using Microsoft.Extensions.DependencyInjection;
using OTPManager.Authenticator.Contracts;
using OTPManager.Authenticator.Implementations;

namespace OTPManager.Authenticator.Configurations
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAuthenticatorServices(this IServiceCollection services)
        {
            services.AddScoped<ITwoFactorAuthenticator, TwoFactorAuthenticator>();
            services.AddScoped<ICacheManager, CacheManager>();
        }
    }
}
