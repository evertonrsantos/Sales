using MediatR;
using FluentValidation;
using SalesApi.Application.Commands.Models;

namespace SalesApi.Application.Commands;

public class CreateProductCommand : IRequest<ProductModel>
{
    public decimal Price { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public string Image { get; set; }
}
