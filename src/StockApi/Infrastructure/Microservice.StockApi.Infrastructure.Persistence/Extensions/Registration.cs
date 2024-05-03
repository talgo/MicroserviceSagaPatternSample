using Microservice.StockApi.Application.Interfaces.Repositories;
using Microservice.StockApi.Infrastructure.Persistence.Context;
using Microservice.StockApi.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.StockApi.Infrastructure.Persistence.Extensions
{
    public static class Registration
    {
        public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StockDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SqlCon")));

            services.AddScoped<IStockRepository, StockRepository>();
            return services;
        }
    }
}
