using MediatR;
using Microsoft.Extensions.Logging;
using SalesApi.Application.Commands;
using SalesApi.Application.Events.Contracts;
using SalesApi.Application.Events.Models;
using SalesApi.Domain.Contracts.Repository;

namespace SalesApi.Application.Handlers;

public class CancelSaleCommandHandler : IRequestHandler<CancelSaleCommand, bool>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IEventPublisher _eventPublisher;

    public CancelSaleCommandHandler(
        ISaleRepository saleRepository,
        IEventPublisher eventPublisher)
    {
        _saleRepository = saleRepository;
        _eventPublisher = eventPublisher;
    }

    public async Task<bool> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(request.Id);
        
        if (sale == null)
        {
            throw new KeyNotFoundException($"Sale with ID {request.Id} not found");
        }
        
        if (sale.IsCancelled)
        {
            throw new InvalidOperationException("Sale is already cancelled");
        }
        
        sale.Cancel();
        
        await _saleRepository.UpdateAsync(sale);
        
        await _eventPublisher.PublishAsync(new SaleCancelledEvent 
        { 
            SaleId = sale.Id,
            CancelledAt = DateTime.Now
        });
        
        return true;
    }
}
