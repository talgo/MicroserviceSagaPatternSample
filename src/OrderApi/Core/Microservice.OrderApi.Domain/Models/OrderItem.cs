using Microservice.Common.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace Microservice.OrderApi.Domain.Models
{
    public class OrderItem : BaseEntity
    {
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public int Count { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
    }
}
