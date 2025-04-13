using MediatR;
using SalesApi.Application.Commands.Models;
using SalesApi.Application.Commands.Validators;

namespace SalesApi.Application.Commands;

public class CreateSaleCommand : IRequest<SaleModel>
{
    public string SaleNumber { get; set; }
    public DateTime SaleDate { get; set; }
    public Guid CustomerId { get; set; }
    public Guid BranchId { get; set; }
    public List<SaleItemCommand> Items { get; set; } = new();
}
