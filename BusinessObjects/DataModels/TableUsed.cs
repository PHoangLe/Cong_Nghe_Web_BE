using System;
using System.Collections.Generic;

namespace BusinessObjects.DataModels
{
    public partial class TableUsed
    {
        public int TableId { get; set; }
        public int ServingId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Serving Serving { get; set; } = null!;
        public virtual DiningTable Table { get; set; } = null!;
    }
}
