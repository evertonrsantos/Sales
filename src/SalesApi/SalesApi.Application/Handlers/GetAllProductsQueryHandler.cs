using AutoMapper;
using MediatR;
using SalesApi.Application.Commands.Models;
using SalesApi.Application.Queries;
using SalesApi.Domain.Contracts.Repository;

namespace SalesApi.Application.Handlers;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductModel>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetAllProductsQueryHandler(
        IProductRepository productRepository,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductModel>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllAsync();
        
        var productDtos = _mapper.Map<IEnumerable<ProductModel>>(products);
        
        return productDtos;
    }
}
