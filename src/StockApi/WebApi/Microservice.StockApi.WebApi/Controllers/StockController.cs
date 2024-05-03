using MediatR;
using Microservice.StockApi.Application.Features.Stocks.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.StockApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StockController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddStockCommand stock)
        {
            return Ok(await _mediator.Send(stock));
        }
    }
}
