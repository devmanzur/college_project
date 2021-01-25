using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Snapkart.Domain.Entities;
using Snapkart.Domain.Interfaces;
using Snapkart.Infrastructure.Brokers;
using Snapkart.Infrastructure.Data;

namespace Snapkart.Infrastructure.Extensions
{
    public static class DiExtensions
    {
        public static void SetupInfrastructureDependencies(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("SqlDatabase")));

            services.AddIdentityCore<AppUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = true;
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequiredUniqueChars = 0;
                    options.User.RequireUniqueEmail = false;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>();
            
            services.AddTransient(typeof(ICrudRepository<>), typeof(EfRepository<>));

            services.AddTransient<IStorageServiceBroker, MinioStorageBroker>();
            services.AddTransient<ISnapQueryRepository, SnapQueryRepository>();
        }
    }
}