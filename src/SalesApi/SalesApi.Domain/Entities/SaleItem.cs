using SalesApi.Domain.Entities.Base;

namespace SalesApi.Domain.Entities;

public class SaleItem : EntityBase
{
    protected SaleItem() { }

    public SaleItem(Guid productId, int quantity, decimal unitPrice)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero", nameof(quantity));

        if (quantity > 20)
            throw new ArgumentException("Cannot sell more than 20 identical items", nameof(quantity));

        if (unitPrice <= 0)
            throw new ArgumentException("Unit price must be greater than zero", nameof(unitPrice));

        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
        IsCancelled = false;

        CalculateTaxAndTotal();
    }

    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal ValueMonetaryTaxApplied { get; private set; }
    public decimal Total { get; private set; }
    public Guid SaleId { get; private set; }
    public bool IsCancelled { get; private set; }

    public virtual Sale Sale { get; private set; }
    public virtual Product Product { get; private set; }

    private void CalculateTaxAndTotal()
    {
        decimal taxRate = CalculateTaxRate();
        decimal subtotal = UnitPrice * Quantity;

        ValueMonetaryTaxApplied = subtotal * (taxRate / 100);
        Total = subtotal + ValueMonetaryTaxApplied;
    }

    private decimal CalculateTaxRate()
    {
        if (Quantity < 4)
            return 0;

        if (Quantity >= 10 && Quantity <= 20)
            return 20;

        return 10;
    }

    public void Cancel()
    {
        IsCancelled = true;
    }
}
