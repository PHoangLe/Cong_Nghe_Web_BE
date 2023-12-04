using System;
using System.Collections.Generic;

namespace BusinessObjects.DataAccess
{
    public partial class Bill
    {
        public int ServingId { get; set; }
        public Guid CreatedBy { get; set; }
        public int TotalPrice { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Account CreatedByNavigation { get; set; } = null!;
        public virtual Serving Serving { get; set; } = null!;
    }
}
