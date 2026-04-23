using System;
using System.Collections.Generic;
using System.Text;

namespace Sklad_project_app.Models
{
    public class WriteOff
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid BatchId { get; set; }
        public DateTime WriteOffDate { get; set; }
        public int Quantity { get; set; }
        public decimal LossAmount { get; set; }
        public string Reason { get; set; }

        public virtual Product Product { get; set; }
        public virtual StockBatch StockBatch { get; set; }
    }
}
