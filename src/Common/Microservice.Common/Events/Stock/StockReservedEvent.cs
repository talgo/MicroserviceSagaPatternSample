using Microservice.Common.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.Common.Events.Stock
{
    public class StockReservedEvent
    {
        public int OrderId { get; set; }
        public int BuyerId { get; set; }
        public PaymentMessage PaymentMessage { get; set; }
        public List<OrderItemMessage> OrderItemMessages { get; set; } = new List<OrderItemMessage>();
    }
}
