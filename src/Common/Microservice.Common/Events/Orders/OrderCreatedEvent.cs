using Microservice.Common.Messages;

namespace Microservice.Common.Events.Orders
{
    public class OrderCreatedEvent
    {
        public int OrderId { get; set; }
        public int BuyerId { get; set; }
        public PaymentMessage PaymentMessage { get; set; }
        public List<OrderItemMessage> OrderItemMessages { get; set; } = new List<OrderItemMessage>();
    }
}
