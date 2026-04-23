using System;
using System.Collections.Generic;
using System.Text;

namespace Sklad_project_app.Models
{
    public class StockBatch
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid SuppliesId { get; set; }
        public int Quantity { get; set; }
        public decimal PurchasePrice { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public decimal DiscountPercent { get; set; }
        public bool IsWrittenOff { get; set; }
        public int TotalDays { get; set; }

        public virtual Product Product { get; set; }
        public virtual Supplies Supply { get; set; }
    }
}
