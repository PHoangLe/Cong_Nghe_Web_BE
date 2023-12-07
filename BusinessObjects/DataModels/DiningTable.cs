using BusinessObjects.DTO;
using System;
using System.Collections.Generic;

namespace BusinessObjects.DataModels
{
    public partial class DiningTable
    {
        public DiningTable()
        {
            TableUseds = new HashSet<TableUsed>();
        }

        public int TableId { get; set; }
        public Guid CreatedBy { get; set; }
        public string Status { get; set; } = null!;
        public string TableNumber { get; set; } = null!;
        public int Capacity { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Account CreatedByNavigation { get; set; } = null!;
        public virtual ICollection<TableUsed> TableUseds { get; set; }
    }
}
