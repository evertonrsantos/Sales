namespace SalesApi.Application.Commands.Models;

public class SaleModel
{
    public Guid Id { get; set; }
    public string SaleNumber { get; set; }
    public DateTime Date { get; set; }
    public Guid CustomerId { get; set; }
    public Guid BranchId { get; set; }
    public decimal TotalAmount { get; set; }
    public bool Cancelled { get; set; }
    public List<SaleItemModel> Items { get; set; } = new();
}
