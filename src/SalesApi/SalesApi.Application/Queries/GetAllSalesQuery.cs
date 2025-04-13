using MediatR;
using SalesApi.Application.Commands.Models;

namespace SalesApi.Application.Queries;

public class GetAllSalesQuery : IRequest<IEnumerable<SaleModel>>
{
}
