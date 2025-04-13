namespace SalesApi.Application.Events.Models
{
    public class SaleCreatedEvent
    {
        public Guid SaleId { get; set; }
        public Guid CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
