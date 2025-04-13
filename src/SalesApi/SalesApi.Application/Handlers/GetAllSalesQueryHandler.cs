using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SalesApi.Application.Commands.Models;
using SalesApi.Application.Queries;
using SalesApi.Domain.Contracts.Repository;

namespace SalesApi.Application.Handlers;

public class GetAllSalesQueryHandler : IRequestHandler<GetAllSalesQuery, IEnumerable<SaleModel>>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    public GetAllSalesQueryHandler(
        ISaleRepository saleRepository,
        IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SaleModel>> Handle(GetAllSalesQuery request, CancellationToken cancellationToken)
    {
        var sales = await _saleRepository.GetAllAsync();
        
        var models = _mapper.Map<IEnumerable<SaleModel>>(sales);
        
        return models;
    }
}
