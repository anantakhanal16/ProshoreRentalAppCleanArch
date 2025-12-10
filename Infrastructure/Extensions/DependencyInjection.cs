using ApplicationLayer.Interfaces;
using ApplicationLayer.Service;
using Core.Entities;
using Core.Interfaces;
using Core.Repositories;
using Infrastructure.Data;
using Infrastructure.Implementation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjection
{
    public static class InfraDependency
    {
        public static IServiceCollection InfraServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IListingRepository, ListingRepository>();

            services.AddScoped<IFileService, FileService>();
       
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }, ServiceLifetime.Scoped);

            // Configure Identity
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            return services;
        }
    }
}



