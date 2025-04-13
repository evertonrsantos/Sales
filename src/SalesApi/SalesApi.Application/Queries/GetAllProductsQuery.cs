using MediatR;
using SalesApi.Application.Commands.Models;

namespace SalesApi.Application.Queries;

public class GetAllProductsQuery : IRequest<IEnumerable<ProductModel>>
{
}
