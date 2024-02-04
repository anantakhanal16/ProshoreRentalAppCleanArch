using ApplicationLayer.Interfaces;
using ApplicationLayer.Service;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationLayer.ApplicationLayerDI
{
    public static class ApplicationLayerDependencies
    {
        public static IServiceCollection ApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>();
            services.AddScoped<IListingService, ListingService>();
            return services;
        }
    }
}
