using MassTransit;
using Microservice.Common.Events.Payment;
using Microservice.OrderApi.Application.Interfaces.Repositories;
using Microservice.OrderApi.Infrastructure.Persistence.Context;
using Microservice.OrderApi.Infrastructure.Persistence.Repositories;

namespace Microservice.OrderApi.WebApi.Consumers
{
    public class PaymentComplatedEventConsumer : IConsumer<PaymentComplatedEvent>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<PaymentComplatedEventConsumer> _logger;

        public PaymentComplatedEventConsumer(IOrderRepository orderRepository, ILogger<PaymentComplatedEventConsumer> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<PaymentComplatedEvent> context)
        {
            var order =await _orderRepository.FindAsync(context.Message.OrderId);

            if (order != null) {
                order.Status = Common.Models.OrderStatus.Completed;
               await _orderRepository.UpdateAsync(order, default);

                _logger.LogInformation($"Order (Id={context.Message.OrderId}) status changed: {order.Status}");
            }
            else
            {
                _logger.LogError($"Order (Id={context.Message.OrderId}) not found!");
            }
        }
    }
}
