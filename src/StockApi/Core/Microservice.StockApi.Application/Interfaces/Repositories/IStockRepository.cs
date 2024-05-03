using Microservice.Common.Core;
using Microservice.StockApi.Domain.Models;

namespace Microservice.StockApi.Application.Interfaces.Repositories
{
    public interface IStockRepository : IBaseGenericRepository<Stock>
    {
    }
}
