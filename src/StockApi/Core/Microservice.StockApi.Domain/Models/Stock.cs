using Microservice.Common.Core;

namespace Microservice.StockApi.Domain.Models
{
    public class Stock : BaseEntity
    {
        public int ProductId { get; set; }
        public int Count { get; set; }
    }
}
