using System;
using System.Collections.Generic;

namespace BusinessObjects.DataModels
{
    public partial class Permission
    {
        public Permission()
        {
            Permits = new HashSet<Permit>();
        }

        public int PermissionId { get; set; }
        public string PermissionName { get; set; } = null!;

        public virtual ICollection<Permit> Permits { get; set; }
    }
}
