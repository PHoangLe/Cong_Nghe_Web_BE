using System;
using System.Collections.Generic;

namespace BusinessObjects.DataModels
{
    public partial class FoodOrder
    {
        public int FoodOrderId { get; set; }
        public int FoodId { get; set; }
        public int ServingId { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; } = null!;
        public string? Note { get; set; }
        public int Price { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Food Food { get; set; } = null!;
        public virtual Serving Serving { get; set; } = null!;
    }
}
