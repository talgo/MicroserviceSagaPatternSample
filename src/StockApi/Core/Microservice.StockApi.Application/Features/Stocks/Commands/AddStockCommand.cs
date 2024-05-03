using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.StockApi.Application.Features.Stocks.Commands
{
    public class AddStockCommand : IRequest<int>
    {
        public int ProductId { get; set; }
        public int Count { get; set; }
    }
}
