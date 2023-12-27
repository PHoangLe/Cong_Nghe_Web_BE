using System;
using System.Collections.Generic;

namespace BusinessObjects.DataModels
{
    public partial class Serving
    {
        public Serving()
        {
            Bills = new HashSet<Bill>();
            FeedBacks = new HashSet<FeedBack>();
            FoodOrders = new HashSet<FoodOrder>();
            TableUseds = new HashSet<TableUsed>();
        }

        public int ServingId { get; set; }
        public Guid CreatedBy { get; set; }
        public int NumberOfCutomer { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime? TimeOut { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Account CreatedByNavigation { get; set; } = null!;
        public virtual ICollection<Bill> Bills { get; set; }
        public virtual ICollection<FeedBack> FeedBacks { get; set; }
        public virtual ICollection<FoodOrder> FoodOrders { get; set; }
        public virtual ICollection<TableUsed> TableUseds { get; set; }
    }
}
