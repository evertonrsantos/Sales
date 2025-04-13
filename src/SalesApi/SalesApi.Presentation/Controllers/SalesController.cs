using MediatR;
using Microsoft.AspNetCore.Mvc;
using SalesApi.Application.Commands;
using SalesApi.Application.Queries;

namespace SalesApi.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class SalesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<SalesController> _logger;

    public SalesController(IMediator mediator, ILogger<SalesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all sales
    /// </summary>
    /// <returns>List of all sales</returns>
    /// <response code="200">Returns the list of sales</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSales()
    {
        var sales = await _mediator.Send(new GetAllSalesQuery());
        
        return Ok(new { data = sales });
    }

    /// <summary>
    /// Creates a new sale with tax calculation
    /// </summary>
    /// <param name="command">The sale data</param>
    /// <returns>The created sale</returns>
    /// <response code="201">Returns the newly created sale</response>
    /// <response code="400">If the sale data is invalid</response>
    /// <response code="404">If a referenced product is not found</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateSale([FromBody] CreateSaleCommand command)
    {
        var sale = await _mediator.Send(command);
        
        var response = new
        {
            data = sale,
            status = "success",
            message = "Venda criada com sucesso"
        };
        
        return CreatedAtAction(nameof(GetSales), response);
    }

    /// <summary>
    /// Cancels a specific sale
    /// </summary>
    /// <param name="id">The ID of the sale to cancel</param>
    /// <returns>Confirmation of cancellation</returns>
    /// <response code="200">The sale was successfully cancelled</response>
    /// <response code="404">If the sale is not found</response>
    /// <response code="400">If the sale is already cancelled</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CancelSale(Guid id)
    {
        await _mediator.Send(new CancelSaleCommand(id));
        
        return Ok(new
        {
            status = "success",
            message = "Sell Cancelled"
        });
    }
}
