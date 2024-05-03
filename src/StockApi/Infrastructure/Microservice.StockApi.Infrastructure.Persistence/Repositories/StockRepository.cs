using Microservice.Common.Core;
using Microservice.StockApi.Application.Interfaces.Repositories;
using Microservice.StockApi.Domain.Models;
using Microservice.StockApi.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.StockApi.Infrastructure.Persistence.Repositories
{
    public class StockRepository : BaseGenericRepository<Stock>, IStockRepository
    {
        public StockRepository(StockDbContext dbContext) : base(dbContext)
        {
        }
    }
}
