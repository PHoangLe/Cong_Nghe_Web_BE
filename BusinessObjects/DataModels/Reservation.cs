using System;
using System.Collections.Generic;

namespace BusinessObjects.DataAccess
{
    public partial class Reservation
    {
        public int ReservationId { get; set; }
        public Guid CreatedBy { get; set; }
        public string CustomerName { get; set; } = null!;
        public DateTime ReservationTime { get; set; }
        public string CustomerPhone { get; set; } = null!;
        public int NumberOfCustomer { get; set; }
        public string? Note { get; set; }
        public string Status { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Account CreatedByNavigation { get; set; } = null!;
    }
}
