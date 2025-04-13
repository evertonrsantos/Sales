using AutoMapper;
using Bogus;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using SalesApi.Application.Commands;
using SalesApi.Application.Handlers;
using SalesApi.Application.Mapping;
using SalesApi.Domain.Contracts.Repository;
using SalesApi.Domain.Entities;
using Xunit;
using SalesApi.Application.Events.Contracts;
using SalesApi.Application.Events.Models;

namespace SalesApi.Tests.UnitTests;

public class CreateProductCommandHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateProductCommandHandler> _logger;
    private readonly IEventPublisher _eventPublisher;
    private readonly CreateProductCommandHandler _handler;
    private readonly Faker _faker;

    public CreateProductCommandHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });
        _mapper = mapperConfig.CreateMapper();
        
        _logger = Substitute.For<ILogger<CreateProductCommandHandler>>();
        _eventPublisher = Substitute.For<IEventPublisher>();
        _handler = new CreateProductCommandHandler(_productRepository, _mapper, _eventPublisher);
        _faker = new Faker();
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldCreateProductAndReturnDto()
    {
        // Arrange
        var command = new CreateProductCommand
        {
            Title = _faker.Commerce.ProductName(),
            Price = _faker.Random.Decimal(1, 1000),
            Description = _faker.Commerce.ProductDescription(),
            Category = _faker.Commerce.Categories(1)[0],
            Image = _faker.Image.PicsumUrl()
        };

        _productRepository
            .CreateAsync(Arg.Any<Product>())
            .Returns(callInfo => callInfo.Arg<Product>());

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Title.Should().Be(command.Title);
        result.Price.Should().Be(command.Price);
        result.Description.Should().Be(command.Description);
        result.Category.Should().Be(command.Category);
        result.Image.Should().Be(command.Image);

        await _productRepository.Received(1).CreateAsync(Arg.Any<Product>());
        await _eventPublisher.Received(1).PublishAsync(Arg.Any<ProductCreatedEvent>());
    }

    [Fact]
    public async Task Handle_CommandWithoutTitle_ShouldGenerateTitle()
    {
        // Arrange
        var command = new CreateProductCommand
        {
            Title = null,
            Price = _faker.Random.Decimal(1, 1000),
            Description = _faker.Commerce.ProductDescription(),
            Category = _faker.Commerce.Categories(1)[0],
            Image = _faker.Image.PicsumUrl()
        };

        _productRepository
            .CreateAsync(Arg.Any<Product>())
            .Returns(callInfo => callInfo.Arg<Product>());

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Title.Should().NotBeNull();
        result.Title.Should().Contain("Product ");
    }
}
