namespace Sklad_project_app.Models
{
    public class Shipment
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public Guid UserId { get; set; }
        public DateTime? ShipmentDate { get; set; }
        public virtual Client Client { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<ShipmentItem> ShipmentItems { get; set; }
    }
}