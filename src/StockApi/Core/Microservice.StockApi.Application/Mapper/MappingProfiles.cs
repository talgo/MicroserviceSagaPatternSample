using AutoMapper;
using Microservice.StockApi.Application.Features.Stocks.Commands;
using Microservice.StockApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.StockApi.Application.Mapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Stock,AddStockCommand>().ReverseMap();
        }
    }
}
