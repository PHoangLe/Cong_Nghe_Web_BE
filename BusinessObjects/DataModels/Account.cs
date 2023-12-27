using System;
using System.Collections.Generic;
using BusinessObjects.Enum;

namespace BusinessObjects.DataModels
{
    public partial class Account
    {
        public Account()
        {
            Bills = new HashSet<Bill>();
            DiningTables = new HashSet<DiningTable>();
            Permits = new HashSet<Permit>();
            Reservations = new HashSet<Reservation>();
            Servings = new HashSet<Serving>();
        }

        public Guid AccountId { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Role { get; set; } = EnumRole.STAFF.ToString();
        public DateOnly? DateOfBirth { get; set; }
        public bool? IsBlock { get; set; }
        public bool? Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Avatar { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Bill> Bills { get; set; }
        public virtual ICollection<DiningTable> DiningTables { get; set; }
        public virtual ICollection<Permit> Permits { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<Serving> Servings { get; set; }
    }
}
