﻿namespace SalesApi.Application.Commands.Models
{
    public class SaleItemModel
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal ValueMonetaryTaxApplied { get; set; }
        public decimal Total { get; set; }
        public Guid SaleId { get; set; }
        public bool IsCancelled { get; set; }
    }
}
