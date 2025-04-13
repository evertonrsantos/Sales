using SalesApi.Domain.Entities.Base;

namespace SalesApi.Domain.Entities;

public class Sale : EntityBase
{
    protected Sale() { }

    public Sale(string saleNumber, DateTime date, Guid customerId, Guid branchId, List<SaleItem> items)
    {
        Items = [];
        SaleNumber = saleNumber;
        Date = date;
        CustomerId = customerId;
        BranchId = branchId;
        IsCancelled = false;

        foreach (var item in items)
        {
            AddItem(item);
        }

        CalculateTotalAmount();
    }

    public string SaleNumber { get; private set; } = null!;
    public DateTime Date { get; private set; }
    public Guid CustomerId { get; private set; }
    public Guid BranchId { get; private set; }
    public decimal TotalAmount { get; private set; }
    public bool IsCancelled { get; private set; }

    public virtual ICollection<SaleItem> Items { get; private set; }


    public void AddItem(SaleItem item)
    {
        Items.Add(item);
        CalculateTotalAmount();
    }

    public void Cancel()
    {
        if (IsCancelled)
            throw new InvalidOperationException("Sale is already cancelled");

        IsCancelled = true;
    }

    private void CalculateTotalAmount()
    {
        TotalAmount = Items.Sum(item => item.Total);
    }
}
