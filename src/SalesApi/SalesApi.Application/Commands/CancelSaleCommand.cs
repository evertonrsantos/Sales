using MediatR;
using FluentValidation;

namespace SalesApi.Application.Commands;

public class CancelSaleCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    
    public CancelSaleCommand(Guid id)
    {
        Id = id;
    }
}
