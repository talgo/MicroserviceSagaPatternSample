using Microservice.Common.Core;
using Microservice.OrderApi.Application.Interfaces.Repositories;
using Microservice.OrderApi.Domain.Models;
using Microservice.OrderApi.Infrastructure.Persistence.Context;

namespace Microservice.OrderApi.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : BaseGenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(OrderDbContext dbContext) : base(dbContext)
        {
        }
    }
}
