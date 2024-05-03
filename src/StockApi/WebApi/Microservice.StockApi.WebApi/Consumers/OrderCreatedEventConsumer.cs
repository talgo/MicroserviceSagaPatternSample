using MassTransit;
using Microservice.Common.Constants.RabbitMQ;
using Microservice.Common.Events.Orders;
using Microservice.Common.Events.Stock;
using Microservice.StockApi.Application.Interfaces.Repositories;

namespace Microservice.StockApi.WebApi.Consumers
{
    public class OrderCreatedEventConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly ILogger<OrderCreatedEventConsumer> _logger;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IPublishEndpoint _publishEndPoint;
        private readonly IStockRepository _stockRepository;

        public OrderCreatedEventConsumer(ILogger<OrderCreatedEventConsumer> logger, ISendEndpointProvider sendEndpointProvider, IPublishEndpoint publishEndPoint, IStockRepository stockRepository)
        {
            _logger = logger;
            _sendEndpointProvider = sendEndpointProvider;
            _publishEndPoint = publishEndPoint;
            _stockRepository = stockRepository;
        }

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var stockResult = new List<bool>();

            foreach (var item in context.Message.OrderItemMessages)
            {
                stockResult.Add(await _stockRepository.AnyAsync(x => x.ProductId == item.ProductId && x.Count > item.Count, CancellationToken.None));
            }

            if(stockResult.All(x=> x.Equals(true)))
            {
                foreach (var item in context.Message.OrderItemMessages)
                {
                    var stock = await _stockRepository.FirstOrDefaultAsync(x => x.ProductId == item.ProductId);

                    if(stock is not null)
                    {
                        stock.Count -= item.Count;
                        await _stockRepository.UpdateAsync(stock, CancellationToken.None);

                        _logger.LogInformation($"Stock was reserved for BuyerId: {context.Message.BuyerId}");

                        var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{RabbitMQSettingsConst.StockReservedEventQueueName}"));

                        StockReservedEvent stockReservedEvent = new StockReservedEvent()
                        {
                            PaymentMessage = context.Message.PaymentMessage,
                            BuyerId = context.Message.BuyerId,
                            OrderId = context.Message.OrderId,
                            OrderItemMessages = context.Message.OrderItemMessages,
                        };

                        await sendEndpoint.Send(stockReservedEvent);
                    }
                }
            }
            else
            {
                await _publishEndPoint.Publish(new StockNotReservedEvent()
                {
                    BuyerId = context.Message.BuyerId,
                    OrderId = context.Message.OrderId,
                    Message = "Not enough stock"
                });
            }
        }
    }
}
