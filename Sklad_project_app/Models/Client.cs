namespace Sklad_project_app.Models
{
    public class Client
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Shipment> Shipments { get; set; }
    }
}
