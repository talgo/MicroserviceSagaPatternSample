using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Microservice.OrderApi.Application.Extensions
{
    public static class Registration
    {
        public static IServiceCollection AddApplicationRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            var assm = Assembly.GetExecutingAssembly();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assm));
            services.AddAutoMapper(assm);

            return services;
        }
    }
}
