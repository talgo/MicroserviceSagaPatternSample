using MassTransit;
using Microservice.Common.Events.Stock;
using Microservice.OrderApi.Application.Interfaces.Repositories;

namespace Microservice.OrderApi.WebApi.Consumers
{
    public class StockNotReservedEventConsumer : IConsumer<StockNotReservedEvent>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<StockNotReservedEventConsumer> _logger;

        public StockNotReservedEventConsumer(IOrderRepository orderRepository, ILogger<StockNotReservedEventConsumer> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<StockNotReservedEvent> context)
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
