using DeadLinerWebApp.BLL.Interfaces;
using DeadLinerWebApp.BLL.Services;
using DeadLinerWebApp.DAL.Domain;
using DeadLinerWebApp.DAL.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DeadLinerWebApp.Helper
{
    public static class AddServicesExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IAuthorization, Authorization>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}