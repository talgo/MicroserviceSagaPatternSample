using Microservice.Common.Core;
using Microservice.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.OrderApi.Domain.Models
{
    public class Order : BaseEntity
    {
        public int BuyerId { get; set; }
        public virtual ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
        public OrderStatus Status { get; set; } = OrderStatus.Suspend;
        public string Fail { get; set; }
        public Address Address { get; set; }
    }
}
