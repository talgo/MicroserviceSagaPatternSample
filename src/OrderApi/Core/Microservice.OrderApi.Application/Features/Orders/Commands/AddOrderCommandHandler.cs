using AutoMapper;
using MassTransit;
using MediatR;
using Microservice.Common.Events.Orders;
using Microservice.Common.Messages;
using Microservice.OrderApi.Application.Interfaces.Repositories;
using Microservice.OrderApi.Domain.Models;

namespace Microservice.OrderApi.Application.Features.Orders.Commands
{
    public class AddOrderCommandHandler : IRequestHandler<AddOrderCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public AddOrderCommandHandler(IMapper mapper, IOrderRepository orderRepository, IPublishEndpoint publishEndpoint)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<int> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Order>(request);

            int addedRowCount = await _orderRepository.AddAsync(order, cancellationToken);

            if (addedRowCount != 0)
            {
                var orderCreatedEvent = _mapper.Map<OrderCreatedEvent>(request);
                orderCreatedEvent.OrderItemMessages = _mapper.Map<List<OrderItemMessage>>(request.Items);
                orderCreatedEvent.PaymentMessage = _mapper.Map<PaymentMessage>(request.Payment);
                orderCreatedEvent.OrderId = order.Id;

                await _publishEndpoint.Publish(orderCreatedEvent, cancellationToken);
            }

            return addedRowCount;
        }
    }
}
