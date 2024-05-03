using AutoMapper;
using MediatR;
using Microservice.StockApi.Application.Interfaces.Repositories;
using Microservice.StockApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.StockApi.Application.Features.Stocks.Commands
{
    public class AddStockCommandHandler : IRequestHandler<AddStockCommand, int>
    {
        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;

        public AddStockCommandHandler(IStockRepository stockRepository, IMapper mapper)
        {
            _stockRepository = stockRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddStockCommand request, CancellationToken cancellationToken)
        {
            int result = 0;
            Stock stockDb = await _stockRepository.FirstOrDefaultAsync(x => x.ProductId == request.ProductId);

            if (stockDb == null)
            {
                stockDb = _mapper.Map<Stock>(request);
                result = await _stockRepository.AddAsync(stockDb, cancellationToken);
            }
            else
            {
                stockDb.Count += request.Count;
                result = await _stockRepository.UpdateAsync(stockDb, cancellationToken);
            }

            return result;
        }
    }
}
