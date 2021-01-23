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
        public static void SetupInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("SqlDatabase")));

            services.AddIdentityCore<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddTransient(typeof(ICrudRepository<>), typeof(EfRepository<>));

            services.AddTransient<IImageServerBroker, MinioImageServer>();
        }
    }
}