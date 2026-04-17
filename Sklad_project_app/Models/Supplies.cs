using System;

namespace Sklad_project_app.Models
{
    public class Supplies
    {
        public Guid Id { get; set; }
        public DateTime SuppliesDate { get; set; } 
        public Guid UserId { get; set; } 
        public virtual User User { get; set; }
        public virtual ICollection<SuppliesItem> SuppliesItems { get; set; }
    }
}
