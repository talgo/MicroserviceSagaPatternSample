using MassTransit;
using Microservice.Common.Events.Payment;
using Microservice.StockApi.Application.Interfaces.Repositories;

namespace Microservice.StockApi.WebApi.Consumers
{
    public class PaymentFailedEventConsumer : IConsumer<PaymentFailedEvent>
    {
        private readonly IStockRepository _stockRepository;
        private readonly ILogger<PaymentFailedEventConsumer> _logger;

        public PaymentFailedEventConsumer(IStockRepository stockRepository, ILogger<PaymentFailedEventConsumer> logger)
        {
            _stockRepository = stockRepository;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<PaymentFailedEvent> context)
        {
            foreach (var item in context.Message.OrderItemMessages)
            {
                var stock = await _stockRepository.FirstOrDefaultAsync(x => x.ProductId == item.ProductId);

                if (stock != null)
                {
                    stock.Count += item.Count;
                    await _stockRepository.UpdateAsync(stock, default);
                }
            }

            _logger.LogInformation($"Stock was released for Order Id ({context.Message.OrderId})");
        }
    }
}
