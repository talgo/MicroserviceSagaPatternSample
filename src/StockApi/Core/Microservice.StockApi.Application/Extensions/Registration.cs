using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.StockApi.Application.Extensions
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
