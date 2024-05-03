using AutoMapper;
using Microservice.Common.Events.Orders;
using Microservice.Common.Messages;
using Microservice.OrderApi.Application.Features.Orders.Commands;
using Microservice.OrderApi.Domain.Models;

namespace Microservice.OrderApi.Application.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Order, AddOrderCommand>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
            CreateMap<PaymentDto, PaymentMessage>().ReverseMap();
            CreateMap<OrderItemDto, OrderItemMessage>().ReverseMap();
            CreateMap<OrderCreatedEvent, AddOrderCommand>().ReverseMap();
        }
    }
}
