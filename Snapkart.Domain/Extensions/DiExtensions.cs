using Microsoft.Extensions.DependencyInjection;
using Snapkart.Domain.Interfaces;
using Snapkart.Domain.Services;

namespace Snapkart.Domain.Extensions
{
    public static class DiExtensions
    {
        public static void SetupDomainDependencies(this IServiceCollection services)
        {
            services.AddTransient<IAppUserService, AppUserService>();
            services.AddTransient<ICategoryService, CategoryService>();
        }
    }
}