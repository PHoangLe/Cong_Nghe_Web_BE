using System;
using System.Collections.Generic;

namespace BusinessObjects.DataModels
{
    public partial class FeedBack
    {
        public int FeedBackId { get; set; }
        public int ServingId { get; set; }
        public int Rating { get; set; }
        public string? Message { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Serving Serving { get; set; } = null!;
    }
}
