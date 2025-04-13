using AutoMapper;
using MediatR;
using SalesApi.Application.Commands;
using SalesApi.Application.Commands.Models;
using SalesApi.Application.Events.Contracts;
using SalesApi.Application.Events.Models;
using SalesApi.Domain.Contracts.Repository;
using SalesApi.Domain.Entities;

namespace SalesApi.Application.Handlers;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductModel>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly IEventPublisher _eventPublisher;

    public CreateProductCommandHandler(
        IProductRepository productRepository,
        IMapper mapper,
        IEventPublisher eventPublisher)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _eventPublisher = eventPublisher;
    }

    public async Task<ProductModel> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var title = string.IsNullOrEmpty(request.Title)
            ? $"Product {Guid.NewGuid()}"
            : request.Title;

        var product = new Product(
            title,
            request.Price,
            request.Description ?? string.Empty,
            request.Category ?? string.Empty,
            request.Image ?? string.Empty
        );

        var createdProduct = await _productRepository.CreateAsync(product);

        var productDto = _mapper.Map<ProductModel>(createdProduct);

        await _eventPublisher.PublishAsync(new ProductCreatedEvent
        {
            ProductId = productDto.Id,
            Title = productDto.Title,
            Price = productDto.Price
        });

        return productDto;
    }
}
