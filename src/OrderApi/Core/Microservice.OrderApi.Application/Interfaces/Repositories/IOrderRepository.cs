using Microservice.Common.Core;
using Microservice.OrderApi.Domain.Models;

namespace Microservice.OrderApi.Application.Interfaces.Repositories
{
    public interface IOrderRepository : IBaseGenericRepository<Order>
    {
    }
}
