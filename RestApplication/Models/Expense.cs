﻿using System;

namespace RestApplication.Models
{
    public class Expense
    {
        public string CostCentre { get; set; } = "UNKNOWN";
        public decimal Total { get; set; }
        public string PaymentMethod { get; set; }
        public string Vendor { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public decimal SalesTax { get; set; }
        public decimal TotalExcludingTax { get; set; }
    }
}