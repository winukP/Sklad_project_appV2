namespace Sklad_project_app.Models
{
    public class Stock
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public decimal PurchasePrice { get; set; }
        public int Rest { get; set; }
        public virtual Product Product { get; set; }
    }
}