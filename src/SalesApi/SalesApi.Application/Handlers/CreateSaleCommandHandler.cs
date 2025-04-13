using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SalesApi.Application.Commands;
using SalesApi.Application.Commands.Models;
using SalesApi.Application.Events.Contracts;
using SalesApi.Application.Events.Models;
using SalesApi.Domain.Contracts.Repository;
using SalesApi.Domain.Entities;

namespace SalesApi.Application.Handlers;

public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, SaleModel>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly IEventPublisher _eventPublisher;

    public CreateSaleCommandHandler(
        ISaleRepository saleRepository,
        IProductRepository productRepository,
        IMapper mapper,
        IEventPublisher eventPublisher)
    {
        _saleRepository = saleRepository;
        _productRepository = productRepository;
        _mapper = mapper;
        _eventPublisher = eventPublisher;
    }

    public async Task<SaleModel> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        foreach (var item in request.Items)
        {
            var product = await _productRepository.GetByIdAsync(item.ProductId);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {item.ProductId} not found");
            }
        }
        
        var saleItems = new List<SaleItem>();
        foreach (var item in request.Items)
        {
            try
            {
                saleItems.Add(new SaleItem(
                    item.ProductId, 
                    item.Quantity, 
                    item.UnitPrice
                ));
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException($"Invalid sale item: {ex.Message}", ex);
            }
        }
        
        var sale = new Sale(
            request.SaleNumber ?? $"{new Random().Next(1000, 9999)}",
            request.SaleDate == default ? DateTime.Now : request.SaleDate.ToLocalTime(),
            request.CustomerId,
            request.BranchId,
            saleItems
        );

        try
        {
            var createdSale = await _saleRepository.CreateAsync(sale);

            var saleDto = _mapper.Map<SaleModel>(createdSale);

            await _eventPublisher.PublishAsync(new SaleCreatedEvent
            {
                SaleId = saleDto.Id,
                CustomerId = saleDto.CustomerId,
                TotalAmount = saleDto.TotalAmount
            });

            return saleDto;
        }
        catch (Exception ex)
        {
            throw ex;
        }   
    }
}
