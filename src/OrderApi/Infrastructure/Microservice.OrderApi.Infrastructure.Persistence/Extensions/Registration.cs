using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microservice.OrderApi.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microservice.OrderApi.Application.Interfaces.Repositories;
using Microservice.OrderApi.Infrastructure.Persistence.Repositories;

namespace Microservice.OrderApi.Infrastructure.Persistence.Extensions
{
    public static class Registration
    {
        public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SqlCon"))
            );

            services.AddScoped<IOrderRepository, OrderRepository>();

            return services;
        }
    }
}