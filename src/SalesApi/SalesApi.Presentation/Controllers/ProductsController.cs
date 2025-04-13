using MediatR;
using Microsoft.AspNetCore.Mvc;
using SalesApi.Application.Commands;
using SalesApi.Application.Queries;

namespace SalesApi.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(IMediator mediator, ILogger<ProductsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all products
    /// </summary>
    /// <returns>List of all active products</returns>
    /// <response code="200">Returns the list of products</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _mediator.Send(new GetAllProductsQuery());
        
        return Ok(new { data = products });
    }

    /// <summary>
    /// Creates a new product
    /// </summary>
    /// <param name="command">The product data</param>
    /// <returns>The created product</returns>
    /// <response code="201">Returns the newly created product</response>
    /// <response code="400">If the product data is invalid</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
    {
        var product = await _mediator.Send(command);
        
        var response = new
        {
            data = product,
            status = "success",
            message = "Produto criado com sucesso"
        };
        
        return CreatedAtAction(nameof(GetProducts), response);
    }
}
