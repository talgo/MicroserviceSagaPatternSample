using MassTransit;
using Microservice.Common.Events.Payment;
using Microservice.Common.Events.Stock;

namespace Microservice.PaymentApi.WebApi.Consumers
{
    public class StockReservedEventConsumer : IConsumer<StockReservedEvent>
    {
        private readonly ILogger<StockReservedEventConsumer> _logger;
        private readonly IPublishEndpoint _publishEndpoint;

        public StockReservedEventConsumer(ILogger<StockReservedEventConsumer> logger, IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<StockReservedEvent> context)
        {
            decimal balance = 3000m;

            if (balance > context.Message.PaymentMessage.TotalPrice)
            {
                _logger.LogInformation($"{context.Message.PaymentMessage.TotalPrice} TL was withdrawn from credit card for user id= {context.Message.BuyerId}");

                await _publishEndpoint.Publish(new PaymentComplatedEvent() { BuyerId = context.Message.BuyerId, OrderId = context.Message.OrderId });
            }
            else
            {
                _logger.LogInformation($"{context.Message.PaymentMessage.TotalPrice} TL was not withdrawn from credit card for user id= {context.Message.BuyerId}");

                await _publishEndpoint.Publish(new PaymentFailedEvent() { BuyerId = context.Message.BuyerId, OrderId = context.Message.OrderId, Message = "Not enough credit", OrderItemMessages = context.Message.OrderItemMessages });
            }
        }
    }
}
