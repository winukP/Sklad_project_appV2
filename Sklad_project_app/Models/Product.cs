namespace Sklad_project_app.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Article { get; set; }
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public Guid UnitId { get; set; }
        public virtual Category Category { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual Stock Stock { get; set; }
    }
}