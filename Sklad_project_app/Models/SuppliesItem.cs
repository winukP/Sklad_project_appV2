using System;

namespace Sklad_project_app.Models
{
    public class SuppliesItem
    {
        public Guid Id { get; set; }
        public Guid SuppliesId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal PurchasePrice { get; set; }
        public virtual Supplies Supplies { get; set; } 
        public virtual Product Product { get; set; }
    }
}
