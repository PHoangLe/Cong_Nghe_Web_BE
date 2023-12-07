using System;
using System.Collections.Generic;

namespace BusinessObjects.DataModels
{
    public partial class Food
    {
        public Food()
        {
            FoodOrders = new HashSet<FoodOrder>();
        }

        public int FoodId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public int Price { get; set; }
        public string? Recipe { get; set; }
        public string? Image { get; set; }
        public string Status { get; set; } = null!;

        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<FoodOrder> FoodOrders { get; set; }
    }
}
