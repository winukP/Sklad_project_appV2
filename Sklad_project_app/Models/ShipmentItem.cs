namespace Sklad_project_app.Models
{
    public class ShipmentItem
    {
        public Guid Id { get; set; }
        public Guid ShipmentId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public virtual Shipment Shipment { get; set; }
        public virtual Product Product { get; set; }
    }
}