namespace SalesApi.Application.Events.Models
{
    public class SaleCancelledEvent
    {
        public Guid SaleId { get; set; }
        public DateTime CancelledAt { get; set; }
    }
}
