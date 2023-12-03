using Microsoft.Extensions.DependencyInjection;
using OTPManager.Application.Services;

namespace OTPManager.Application.Configurations
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IOTPAuthenticator, OTPAuthenticator>();
        }
    }
}
