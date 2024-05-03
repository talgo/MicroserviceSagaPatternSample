using MediatR;
using Microservice.OrderApi.Application.Features.Orders.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.OrderApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddOrderCommand order)
        {
            return Ok(await _mediator.Send(order));
        }
    }
}
