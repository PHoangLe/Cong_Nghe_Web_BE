using System;
using System.Collections.Generic;

namespace BusinessObjects.DataAccess
{
    public partial class Permit
    {
        public int PermissionId { get; set; }
        public Guid AccountId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Account Account { get; set; } = null!;
        public virtual Permission Permission { get; set; } = null!;
    }
}
