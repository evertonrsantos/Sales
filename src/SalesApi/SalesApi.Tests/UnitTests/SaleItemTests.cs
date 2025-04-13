using FluentAssertions;
using SalesApi.Domain.Entities;
using Xunit;

namespace SalesApi.Tests.UnitTests;

public class SaleItemTests
{
    [Fact]
    public void Create_SaleItem_WithValidQuantity_ShouldSucceed()
    {
        // Arrange
        var productId = Guid.NewGuid();
        int quantity = 5;
        decimal unitPrice = 10.0m;

        // Act
        var saleItem = new SaleItem(productId, quantity, unitPrice);

        // Assert
        saleItem.Should().NotBeNull();
        saleItem.ProductId.Should().Be(productId);
        saleItem.Quantity.Should().Be(quantity);
        saleItem.UnitPrice.Should().Be(unitPrice);
        saleItem.IsCancelled.Should().BeFalse();
        
        // Tax rate for 5 items should be 10%
        saleItem.ValueMonetaryTaxApplied.Should().Be(5.0m); // 10% of (5 * 10)
        saleItem.Total.Should().Be(55.0m); // 50 + 5 tax
    }

    [Fact]
    public void Create_SaleItem_WithLessThan4Items_ShouldHaveNoTax()
    {
        // Arrange
        var productId = Guid.NewGuid();
        int quantity = 3;
        decimal unitPrice = 10.0m;

        // Act
        var saleItem = new SaleItem(productId, quantity, unitPrice);

        // Assert
        saleItem.ValueMonetaryTaxApplied.Should().Be(0); // No tax
        saleItem.Total.Should().Be(30.0m); // Just the subtotal
    }

    [Fact]
    public void Create_SaleItem_WithBetween10And20Items_ShouldHave20PercentTax()
    {
        // Arrange
        var productId = Guid.NewGuid();
        int quantity = 15;
        decimal unitPrice = 10.0m;

        // Act
        var saleItem = new SaleItem(productId, quantity, unitPrice);

        // Assert
        saleItem.ValueMonetaryTaxApplied.Should().Be(30.0m); // 20% of (15 * 10)
        saleItem.Total.Should().Be(180.0m); // 150 + 30 tax
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Create_SaleItem_WithInvalidQuantity_ShouldThrowException(int quantity)
    {
        // Arrange
        var productId = Guid.NewGuid();
        decimal unitPrice = 10.0m;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new SaleItem(productId, quantity, unitPrice));
    }

    [Fact]
    public void Create_SaleItem_WithMoreThan20Items_ShouldThrowException()
    {
        // Arrange
        var productId = Guid.NewGuid();
        int quantity = 21;
        decimal unitPrice = 10.0m;

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => new SaleItem(productId, quantity, unitPrice));
        exception.Message.Should().Contain("Cannot sell more than 20 identical items");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Create_SaleItem_WithInvalidUnitPrice_ShouldThrowException(decimal unitPrice)
    {
        // Arrange
        var productId = Guid.NewGuid();
        int quantity = 5;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new SaleItem(productId, quantity, unitPrice));
    }

    [Fact]
    public void Cancel_SaleItem_ShouldSetIsCancelledToTrue()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var saleItem = new SaleItem(productId, 5, 10.0m);

        // Act
        saleItem.Cancel();

        // Assert
        saleItem.IsCancelled.Should().BeTrue();
    }
}
