using AutoMapper;
using MassTransit;
using Microservice.Common.Events.Payment;
using Microservice.OrderApi.Application.Interfaces.Repositories;
using Microservice.OrderApi.Infrastructure.Persistence.Repositories;

namespace Microservice.OrderApi.WebApi.Consumers
{
    public class PaymentFailedEventConsumer : IConsumer<PaymentFailedEvent>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<PaymentFailedEventConsumer> _logger;

        public PaymentFailedEventConsumer(IOrderRepository orderRepository, ILogger<PaymentFailedEventConsumer> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<PaymentFailedEvent> context)
        {
            var order = await _orderRepository.FindAsync(context.Message.OrderId);

            if (order != null)
            {
                order.Status = Common.Models.OrderStatus.Failed;
                order.Fail = context.Message.Message;
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
