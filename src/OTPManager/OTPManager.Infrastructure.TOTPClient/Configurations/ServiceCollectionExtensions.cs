using Microsoft.Extensions.DependencyInjection;
using OTPManager.Infrastructure.TOTPClient.Services;

namespace OTPManager.Infrastructure.TOTPClient.Configurations
{
    public static class ServiceCollectionExtensions
    {
        public static void AddTOTPClientServices(this IServiceCollection services)
        {
            services.AddSingleton<ITOTPClient, Services.TOTPClient>();
        }
    }
}
